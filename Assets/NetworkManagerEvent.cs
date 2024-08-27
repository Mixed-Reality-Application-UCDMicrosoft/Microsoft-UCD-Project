using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.OpenXR;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerEvent : NetworkManager
{
    [Header("Extra")]
    public int minConnections = 2;
    public NetworkManagerState state;

    public MarkerOutputManager arMarker;
    [SerializeField] private RoomPlayer roomPlayerPrefab;
    [SerializeField] private LayoutPlayer layoutPlayerPrefab;

    [Header("Layouts")]
    public GameObject WeddingLayout1;
    public GameObject[] CorporateLayout1;
    public FloorScale floorPrefab;

    [Header("Camera Objects")]
    public GameObject XRRig;
    public GameObject CameraObject;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public List<RoomPlayer> RoomPlayers { get; } = new();
    public List<LayoutPlayer> LayoutPlayers { get; } = new();


    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("Spawnable Prefabs").ToList();
    }

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("Spawnable Prefabs");

        foreach(var prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
        arMarker.gameObject.SetActive(true);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        OnClientConnected?.Invoke();
        
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }
        if (state == NetworkManagerState.RECRUITMENT_FINISED || state == NetworkManagerState.NONE)
        {
            conn.Disconnect();
            return;
        }

    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        
        if (numPlayers < minConnections)
        {
            state = NetworkManagerState.BELOW_MIN;
        }
        var playerInstance = conn.identity.GetComponent<RoomPlayer>();
        RoomPlayers.Remove(playerInstance);
        NotifyReadyState();
        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log($"Adding Player... {conn.connectionId}");
        GameObject obj = Instantiate(roomPlayerPrefab.gameObject);
        RoomPlayer r = obj.GetComponent<RoomPlayer>();
        r.IsLeader = RoomPlayers.Count == 0;
        if(numPlayers > 0)
        {
            r.SelectedLayout = RoomPlayers[0].SelectedLayout;
        }
        if (numPlayers < minConnections-1)
        {
            state = NetworkManagerState.BELOW_MIN;
            Debug.Log($"Still Below Connection for conid {conn.connectionId}");
        } else
        {
            state = NetworkManagerState.READY;
        }
        NetworkServer.AddPlayerForConnection(conn, obj);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        LayoutPlayers.Clear();
    }

    private bool CheckReady()
    {
        if (state == NetworkManagerState.BELOW_MIN)
        {
            Debug.Log($"Below Minimum Rate : {numPlayers} {state}");
            return false;
        }
        if (RoomPlayers[0].SelectedLayout == Layouts.NONE)
        {
            Debug.Log($"Layout not selected");
            return false;
        }
        foreach (var playerInstance in RoomPlayers)
        {
            if (!playerInstance.IsReady)
            {
                //Debug.Log($"{playerInstance.connectionToClient.connectionId} is not ready!");
                return false;
            }
        }
        return true;
    }

    public void DisconnectEverything()
    {
        StopHost();
        StopClient();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [Server]
    public void NotifyReadyState()
    {
        
        foreach(var player in RoomPlayers)
        {
            player.HandleReadyState(CheckReady());            
        }
    }

    public void SendPlayerQRCodePos(Vector3 qrCodePos)
    {
        foreach (var playerInstance in RoomPlayers)
        {
            if (playerInstance.isOwned) playerInstance.UpdateQRCodePosition(qrCodePos);
        }
    }

    [Server]
    public void ChangePort(ushort port)
    {
        var transport = GetComponent<kcp2k.KcpTransport>();
        transport.port = port;
        StopHost();
        StartHost();
    }

    [Server]
    public void StartLayout()
    {
        if (!CheckReady()) return;
        state = NetworkManagerState.RECRUITMENT_FINISED;
        floorPrefab.currentLayout = RoomPlayers[0].SelectedLayout;
        if (RoomPlayers[0].SelectedLayout == Layouts.WEDDING1)
        {
            GameObject lay = Instantiate(WeddingLayout1);
            NetworkServer.Spawn(lay);
            
        }
        else if (RoomPlayers[0].SelectedLayout == Layouts.CORPORATE1)
        {
            foreach(GameObject g in CorporateLayout1)
            {
                GameObject g2 = Instantiate(g);
                NetworkServer.Spawn(g2);
            }
        }
        
        
        GameObject floor = Instantiate(floorPrefab.gameObject);
        NetworkServer.Spawn(floor);
        
        for (int i = RoomPlayers.Count - 1; i >= 0; i--)
        {
            var conn = RoomPlayers[i].connectionToClient;
            GameObject layoutInstantiate = Instantiate(layoutPlayerPrefab.gameObject);
            LayoutPlayer l = layoutInstantiate.GetComponent<LayoutPlayer>();
            l.DisplayName = RoomPlayers[i].DisplayName;
            l.QRCodePos = RoomPlayers[i].QRCodePos;
            l.SelectedLayout = RoomPlayers[i].SelectedLayout;
            l.IsQRScanned = RoomPlayers[i].IsQRScanned;

            RoomPlayers[i].RpcDisable();
            NetworkServer.ReplacePlayerForConnection(conn, layoutInstantiate);

        }
        
    }

}

public enum NetworkManagerState
{
    NONE, BELOW_MIN, READY, MAX, RECRUITMENT_FINISED
}
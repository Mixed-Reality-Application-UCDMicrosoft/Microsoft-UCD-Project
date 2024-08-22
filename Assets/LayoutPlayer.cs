using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class LayoutPlayer : NetworkBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private PlayerHandler playerPrefab;

    [Header("Sync Vars")]
    [SyncVar(hook = nameof(HandleDisplayNameChange))]
    public string DisplayName = "Loading...";
    [SyncVar]
    public bool IsQRScanned = false;
    [SyncVar(hook = nameof(HandleQRCodeChanges))]
    public Vector3 QRCodePos;
    [SyncVar]
    public Layouts SelectedLayout;

    [Header("Other")]
    [SyncVar(hook = nameof(HandleSelectedLayout))]
    public GameObject SelectedLayoutObject;

    private NetworkManagerEvent networkManager;
    private NetworkManagerEvent Network
    {
        get
        {
            if (networkManager != null) return networkManager;
            return networkManager = NetworkManager.singleton as NetworkManagerEvent;
        }
    }

    public override void OnStartClient()
    {
        Network.LayoutPlayers.Add(this);
        if (isOwned) return;
        Vector3 qrPos = Vector3.zero;
        foreach(RoomPlayer r in Network.RoomPlayers)
        {
            if (connectionToServer.connectionId != r.connectionToServer.connectionId) continue;
            qrPos = r.QRCodePos;
            break;
        }
        transform.position = qrPos;

    }
    public override void OnStopClient()
    {
        Network.LayoutPlayers.Remove(this);
        PlayerHandler player = transform.GetChild(0).GetComponent<PlayerHandler>();
        player.xrObject = Network.XRRig;
        player.cameraObject = Network.CameraObject;
        player.enabled = true;
    }

    public override void OnStartAuthority()
    {
        transform.SetParent(Network.XRRig.transform);
    }

    [Server]
    public void StartSpawn()
    {
        Debug.Log($"Runnign Spawn script {Network.LayoutPlayers.Count}");

    }

    [Server]
    public void StartLayout()
    {
        GameObject l = null;
        if (SelectedLayout == Layouts.WEDDING1)
            l = Network.WeddingLayout1;
        l = Instantiate(l, QRCodePos, Quaternion.identity);
        NetworkServer.Spawn(l, connectionToClient);
    }

    private void HandleDisplayNameChange(string oldVal, string newVal)
    {
        // TODO
    }

    private void HandleQRCodeChanges(Vector3 oldVal, Vector3 newVal)
    {
        // TODO
    }

    private void HandleSelectedLayout(GameObject prev, GameObject after)
    {
        after.transform.position = QRCodePos;
    }

    [Server]
    public void SetDisplayName(string name)
    {
        DisplayName = name;
    }


}

/*
[TargetRpc]
    private void TargetPlayerSetPos()
    {
        foreach(var s in Network.SpawnCapsules)
        {
            var ident = s.GetComponent<NetworkIdentity>();
            var conn = ident.connectionToServer.connectionId;
            if (conn == connectionToServer.connectionId)
            {
                s.transform.SetParent(Network.XRRig.transform);
                s.cameraObject = Network.CameraObject;
                s.xrObject = Network.XRRig;
                s.enabled = true;
                continue;
            }
            Vector3 qrPos = Vector3.zero;
            foreach(var l in Network.LayoutPlayers)
            {
                if (conn != l.connectionToServer.connectionId) continue;
                qrPos = l.QRCodePos;
                return;
            }
            s.transform.position = (2 * qrPos) - QRCodePos;
        }
    }
}
*/
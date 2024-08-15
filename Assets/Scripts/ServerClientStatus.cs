using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class ServerClientStatus : MonoBehaviour
{
    public NetworkManager nm;
    public bool isServer;
    public GameObject nextButton;

    public TextMeshProUGUI header;
    public TextMeshProUGUI txt;

    public CurrentState state;

    private bool buttonPressed;

    private string s;
    private List<int> connections;
    public void ReceiveButtonPress()
    {
        buttonPressed = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        state = CurrentState.CONNECTING;
        isServer = NetworkServer.active;
        buttonPressed = false;
        nextButton.SetActive(false);
        if (isServer)
        {
            txt.text = $"Conneting to client";
        }
        connections = new List<int>();
        s = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (state == CurrentState.CONNECTING)
        {
            if (!isServer)
            {
                if (NetworkClient.isConnected && NetworkClient.connection.isReady)
                {
                    txt.text = "Connected To Server and ready!";
                    nextButton.SetActive(true);
                    state = CurrentState.CONNECTED;
                }
            }
        }
        if (isServer)
        {
            foreach (var conn in NetworkServer.connections.Values)
            {
                if (conn.isReady)
                {
                    bool foundValue = false;
                    foreach (int conID in connections)
                        if (conID == conn.connectionId)
                        {
                            foundValue = true;
                            break;
                        }
                    if (!foundValue)
                    {
                        s += $"Client with IP Adress: {conn.address} connected with connection ID: {conn.connectionId} !\n";
                        connections.Add(conn.connectionId);
                        txt.text = s;
                        if (connections.Count > 1)
                        {
                            nextButton.SetActive(true);
                            state = CurrentState.CONNECTED;
                        }
                    }

                }
            }
        }
    }

}

public enum CurrentState 
{
    CONNECTING, CONNECTED

} 
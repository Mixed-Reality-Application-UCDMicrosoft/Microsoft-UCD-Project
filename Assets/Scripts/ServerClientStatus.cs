using System.Collections;
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

    public bool buttonPressed;

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
            else
            {
                foreach (var conn in NetworkServer.connections.Values)
                {
                    if (conn.isReady)
                    {
                        txt.text = $"Client with IP Adress: {conn.address} connected !";
                        nextButton.SetActive(true);
                        state = CurrentState.CONNECTED;
                    }
                }
            }
        } else if (state == CurrentState.CONNECTED)
        {
            if (buttonPressed)
            {
                
            }
        }
    }

}

public enum CurrentState 
{
    CONNECTING, CONNECTED

} 
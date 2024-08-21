using System;
using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class JoinHostManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbyObject;
    [SerializeField] private MRTKTMPInputField ipAddress;
    [SerializeField] private MRTKTMPInputField port;
    [SerializeField] private PressableButton joinButton;
    [SerializeField] private TextMeshProUGUI statusText;

    [SerializeField] private NetworkManagerEvent networkManager;

    [Header("PlayerPrefs")]
    public string playerPrefIPAddress = "IPAddressField";
    public string playerPrefPort = "PortField";

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(playerPrefIPAddress))
            ipAddress.text = PlayerPrefs.GetString(playerPrefIPAddress);
        if (PlayerPrefs.HasKey(playerPrefPort))
            port.text = PlayerPrefs.GetString(playerPrefPort);
        NetworkManagerEvent.OnClientConnected += HandleConnection;
        NetworkManagerEvent.OnClientDisconnected += HandleDisconnection;
    }

    private void OnDisable()
    {
        NetworkManagerEvent.OnClientConnected -= HandleConnection;
        NetworkManagerEvent.OnClientDisconnected -= HandleDisconnection;
    }

    public void OnClick()
    {
        networkManager.networkAddress = ipAddress.text;
        var transport = networkManager.GetComponent<kcp2k.KcpTransport>();
        ushort.TryParse(port.text, out ushort result);
        transport.port = result;

        statusText.text = "Connecting...";
        networkManager.StartClient();
        joinButton.gameObject.SetActive(false);
    }

    private void HandleDisconnection()
    {
        joinButton.gameObject.SetActive(true);
        statusText.text = "Connection Error, please try again";
    }

    private void HandleConnection()
    {
        PlayerPrefs.SetString(playerPrefIPAddress, ipAddress.text);
        PlayerPrefs.SetString(playerPrefPort, port.text);

        //lobbyObject.SetActive(true);
        gameObject.SetActive(false);
    }



}

using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class RoomPlayer : NetworkBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private TextMeshProUGUI[] playerNameTexts = new TextMeshProUGUI[6];
    [SerializeField] private TextMeshProUGUI[] readyTexts = new TextMeshProUGUI[6];
    [SerializeField] private TextMeshProUGUI[] qrCodeScannedTexts = new TextMeshProUGUI[6];

    [Header("Misc UI")]
    [SerializeField] private TextMeshProUGUI currentLayout;
    [SerializeField] private MRTKTMPInputField portField;
    [SerializeField] private GameObject startButton;
    [SerializeField] private TextMeshProUGUI readyButtonText;
    [SerializeField] private GameObject layoutButton;
    [SerializeField] private GameObject portChangeLayout;

    [Header("Sync Vars")]
    [SyncVar(hook = nameof(HandleDisplayNameChange))]
    public string DisplayName = "Loading...";
    [SyncVar(hook = nameof(HandleReadyChange))]
    public bool IsReady = false;
    [SyncVar(hook = nameof(HandleQRScanningChange))]
    public bool IsQRScanned = false;
    [SyncVar]
    public Vector3 QRCodePos;
    [SyncVar(hook = nameof(HandleLayoutChange))]
    public Layouts SelectedLayout = Layouts.NONE;

    private bool isLeader;

    private NetworkManagerEvent networkManager;

    public bool IsLeader
    {
        set
        {
            isLeader = value;
            startButton.SetActive(value);
            portChangeLayout.SetActive(value);
            //layoutButton.SetActive(value);
        }
        get => isLeader;
    }

    private NetworkManagerEvent Network
    {
        get
        {
            if (networkManager != null) return networkManager;
            return networkManager = NetworkManager.singleton as NetworkManagerEvent;
        }
    }

    public override void OnStartAuthority()
    {
        CmdSetDisplayName(NameManager.Name);
    }

    public override void OnStartClient()
    {
        Network.RoomPlayers.Add(this);
        if (isOwned)
            lobbyUI.SetActive(true);
        else lobbyUI.SetActive(false);

        startButton.SetActive(false);
        portChangeLayout.SetActive(IsLeader);

        UpdateUI();
    }

    public override void OnStopClient()
    {
        Network.RoomPlayers.Remove(this);
        UpdateUI();
    }
    public void HandleReadyState(bool val)
    {
        UpdateUI();
        if (IsLeader && val)
            startButton.SetActive(true);
        else
            startButton.SetActive(false);
    }
    private void HandleReadyChange(bool oldVal, bool newVal)
    {
        UpdateUI();
    }

    private void HandleDisplayNameChange(string oldVal, string newVal)
    {
        UpdateUI();
    }

    private void HandleQRScanningChange(bool oldVal, bool newVal)
    {
        UpdateUI();
    }

    private void HandleLayoutChange(Layouts oldVal, Layouts newVal)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!isOwned)
        {
            foreach (var p in Network.RoomPlayers)
            {
                if (p.isOwned)
                {
                    p.UpdateUI();
                    return;
                }
            }
        }

        for (int i = 0; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting...";
            readyTexts[i].text = string.Empty;
            qrCodeScannedTexts[i].text = string.Empty;
        }

        for (int i = 0; i < Network.RoomPlayers.Count; i++)
        {
            playerNameTexts[i].text = Network.RoomPlayers[i].DisplayName;
            readyTexts[i].text = Network.RoomPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
            qrCodeScannedTexts[i].text = Network.RoomPlayers[i].IsQRScanned ? "<color=green>Scanned</color>" : "<color=red>Not Scanned</color>";
        }
        readyButtonText.text = IsReady ? "<color=red>Not Ready</color>" : "<color=green>Ready</color>";

        switch(SelectedLayout)
        {
            case Layouts.NONE:
                currentLayout.text = "<color=red>No layout selected by the host</color>";
                break;
            case Layouts.WEDDING1:
                currentLayout.text = "<color=green>Wedding Layout </color>";
                break;
            default:
                currentLayout.text = "Unknown layout";
                break;
        }
    }

    [Client]
    public void OnReadyButton()
    {
        CmdChangeReady();
    }

    [Client]
    public void OnPortChange()
    {
        ushort.TryParse(portField.text, out ushort result);
        Network.ChangePort(result);
    }
    [Client]
    public void UpdateQRCodePosition(Vector3 qrCodePos)
    {
        CmdUpdateQRCodePosition(qrCodePos);
    }

    [Client]
    public void OnLayoutChange(string a)
    {
        switch(a)
        {
            case "WEDDING1":
                CmdChangeLayout(Layouts.WEDDING1);
                break;
            case "WEDDING2":
                CmdChangeLayout(Layouts.WEDDING2);
                break;
            case "CORPORATE1":
                CmdChangeLayout(Layouts.CORPORATE1);
                break;
            case "CORPORATE2":
                CmdChangeLayout(Layouts.CORPORATE2);
                break;
            case "INFORMAL1":
                CmdChangeLayout(Layouts.INFORMAL1);
                break;
            case "INFORMAL2":
                CmdChangeLayout(Layouts.INFORMAL2);
                break;
            default:
                Debug.LogError($"Invalid Layout Name - ${a}", gameObject);
                break;

        }

    }

    [Client]
    public void OnStartButton()
    {
        CmdStart();
    }

    [Command]
    private void CmdSetDisplayName(string name)
    {
        DisplayName = name;
    }

    [Command]
    private void CmdChangeReady()
    {
        IsReady = !IsReady;
        Network.NotifyReadyState();
    }

    [Command]
    private void CmdUpdateQRCodePosition(Vector3 qrCodePos)
    {
        QRCodePos = qrCodePos;
        IsQRScanned = true;
    }

    [Command]
    private void CmdStart()
    {
        if (Network.RoomPlayers[0].connectionToClient != connectionToClient) return;
        Network.StartLayout();

    }

    [ClientRpc(includeOwner = true)]
    public void RpcDisable()
    {
        Debug.Log("Disabling");
        gameObject.SetActive(false);
    }

    [Command]
    private void CmdChangeLayout(Layouts a )
    {
        
        
            foreach(var p in Network.RoomPlayers)
            {
                p.SelectedLayout = a;
            }
        
        

    }
}

public enum Layouts
{
    NONE, WEDDING1, WEDDING2, CORPORATE1, CORPORATE2, INFORMAL1, INFORMAL2
}
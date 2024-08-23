using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    public GameObject cameraObject;
    public GameObject xrObject;
    public GameObject transformObject;
    public TextMeshProUGUI textObject;

    [SyncVar(hook = nameof(HandleNameChange))]
    public string playerName = "Loading";

    public override void OnStartAuthority()
    {
        textObject.gameObject.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (!isOwned) return;
        transformObject.transform.SetPositionAndRotation(cameraObject.transform.position, cameraObject.transform.rotation);
    }

    private void HandleNameChange(string oldVal, string newVal)
    {
        textObject.text = playerName;
    }

    public void SendName(string displayName)
    {
        CmdSendName(displayName);
    }

    [Command]
    public void CmdSendName(string displayName)
    {
        playerName = displayName;
    }
}

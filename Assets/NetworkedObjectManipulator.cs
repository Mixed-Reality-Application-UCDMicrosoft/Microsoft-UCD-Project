using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkedObjectManipulator : NetworkBehaviour
{
    public GameObject[] colorChangeObjects;
    //public readonly string nameObject;

    [SyncVar(hook = nameof(HandleColorChange))]
    public string CurrentColor = "#FFFFFF";

    public void ChangeColor(string colorString)
    {
        CmdApplyCurrentColor(colorString);
    }


    public void TransferOwnershipObject()
    {
        CmdTransferOwn();
    }

    [Command(requiresAuthority = false)]
    public void CmdTransferOwn(NetworkConnectionToClient sender = null)
    {
        if(sender == null)
        {
            Debug.Log("Sender is null!!");
            return;
        }
        Debug.Log($"Sender ID:{sender.connectionId}");
        NetworkIdentity id = gameObject.GetComponentInParent<NetworkIdentity>();
        id.RemoveClientAuthority();
        id.AssignClientAuthority(sender);
    }

    [Command(requiresAuthority = false)]
    public void CmdApplyCurrentColor(string colorString)
    {
        CurrentColor = colorString;
    }

    private void HandleColorChange(string oldCol, string newCol)
    {
        Color c;
        ColorUtility.TryParseHtmlString(newCol, out c);

        Debug.Log($"System color {oldCol} / {newCol}, {c}");

        for (int i = 0; i < colorChangeObjects.Length; i++)
        {
            colorChangeObjects[i].GetComponent<Renderer>().material.color = c;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkedObjectManipulator : NetworkBehaviour
{
    public GameObject[] colorChangeObjects;
    public bool haveParent = false;
    public readonly string nameObject;

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

    [ClientCallback]
    public void ApplyAll()
    {
        if (!haveParent) return;
        Transform parent = gameObject.transform.parent;
        foreach (NetworkedObjectManipulator m in parent.GetComponentsInChildren<NetworkedObjectManipulator>())
        {
            m.ChangeColor(CurrentColor);
        }

    }

    [Command(requiresAuthority = false)]
    public void CmdTransferOwn(NetworkConnectionToClient sender = null)
    {
        if(sender == null)
        {
            Debug.Log("Sender is null!!");
            return;
        }
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

        for (int i = 0; i < colorChangeObjects.Length; i++)
        {
            colorChangeObjects[i].GetComponent<Renderer>().material.color = c;
        }
    }
}

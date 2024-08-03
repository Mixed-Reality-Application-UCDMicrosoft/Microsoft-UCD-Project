using Mirror;
using UnityEngine;

public class CanvasDialogManager : NetworkBehaviour
{
    // Public GameObject field to be set in the Unity Inspector
    public GameObject viewCata;

    // This method is called when the Negative button is clicked
    public void OnNegativeButtonClicked()
    {
        if (isLocalPlayer)
        {
            CmdShowViewCata();
        }
    }

    // Command to be called on the server to handle the button click
    [Command]
    void CmdShowViewCata()
    {
        RpcShowViewCata();
    }

    // ClientRpc to be called on all clients to update their view
    [ClientRpc]
    void RpcShowViewCata()
    {
        // Activate the GameObject referred by viewCata
        viewCata.SetActive(true);
    }
}

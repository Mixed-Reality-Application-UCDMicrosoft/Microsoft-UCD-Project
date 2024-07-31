using UnityEngine;
using Mirror; // For networking
using MixedReality.Toolkit.UX; // Correct namespace for PressableButton

public class CanvasDialogManager : NetworkBehaviour
{
    public PressableButton negativeButton;
    public GameObject viewCatalogPanel;

    void Start()
    {
        // Ensure the button is set up
        if (negativeButton != null)
        {
            // Subscribe to the ButtonPressed event
            negativeButton.OnClicked.AddListener(OnViewCatalogClicked);
        }
    }

    void OnViewCatalogClicked()
    {
        if (isLocalPlayer)
        {
            CmdViewCatalog();
        }
    }

    [Command]
    void CmdViewCatalog()
    {
        RpcShowCatalog();
    }

    [ClientRpc]
    void RpcShowCatalog()
    {
        viewCatalogPanel.SetActive(true);
    }
}

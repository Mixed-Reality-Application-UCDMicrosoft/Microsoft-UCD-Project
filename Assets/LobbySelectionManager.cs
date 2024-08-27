using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySelectionManager : MonoBehaviour
{
    [SerializeField] private NetworkManagerEvent networkManager;
    [SerializeField] private GameObject joinCanvasObject;

    public void OnHostButton()
    {
        networkManager.state = NetworkManagerState.BELOW_MIN;
        networkManager.StartHost();
        gameObject.SetActive(false);
    }

    public void OnJoinButton()
    {
        joinCanvasObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

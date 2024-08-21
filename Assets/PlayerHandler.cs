using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    public GameObject cameraObject;
    public GameObject xrObject;
    public GameObject transformObject;

    public void FixedUpdate()
    {
        if (!isOwned) return;
        transformObject.transform.SetPositionAndRotation(cameraObject.transform.position, cameraObject.transform.rotation);
    }
}

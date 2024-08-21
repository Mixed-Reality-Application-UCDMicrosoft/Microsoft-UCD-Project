using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class AttatchToQRCode : NetworkBehaviour
{
    public void Start()
    {
        var s = NetworkManager.singleton as NetworkManagerEvent;
        var pos = s.arMarker.CoOrds;
        transform.position = pos;
    }
}

using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetNetworkParameters : MonoBehaviour
{
    public Text ipAddress;
    public Text port;
    public NetworkManager nm;

    public void SetNetwork()
    {
        nm.networkAddress = ipAddress.text;
        ushort.TryParse(port.text, out ushort result);

        nm.GetComponent<kcp2k.KcpTransport>().port = result;


    }
}

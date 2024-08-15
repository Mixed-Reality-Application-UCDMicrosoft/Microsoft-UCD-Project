using System.Collections;
using System.Collections.Generic;
using Mirror;
using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;


public class SetNetworkParameters : MonoBehaviour
{
    public MRTKTMPInputField ipAddress;
    public MRTKTMPInputField port;
    public PressableButton isServer;
    public NetworkManager nm;
    public string hello;

    public void SetNetwork()
    {
        if (isServer.IsToggled)
        {
            ushort.TryParse(port.text, out ushort result);
            nm.GetComponent<kcp2k.KcpTransport>().port = result;

            nm.StartHost();
        } else
        {
            nm.networkAddress = ipAddress.text;
            ushort.TryParse(port.text, out ushort result);

            nm.GetComponent<kcp2k.KcpTransport>().port = result;
            nm.StartClient();
        }

        


    }
}

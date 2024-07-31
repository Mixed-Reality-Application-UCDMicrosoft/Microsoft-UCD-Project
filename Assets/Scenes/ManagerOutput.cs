using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.OpenXR;
using TMPro;

public class ManagerOutput : MonoBehaviour
{

    private ARMarkerManager m_arMarkerManager;
    public TextMeshProUGUI debugText;
    // Start is called before the first frame update
    void Start()
    {
        m_arMarkerManager = GetComponent<ARMarkerManager>();
        m_arMarkerManager.markersChanged += OnQRCodesChanged;
    }

    void OnQRCodesChanged(ARMarkersChangedEventArgs args)
    {
        foreach (ARMarker qrCode in args.updated)
        {
            string s = $"Position: {qrCode.transform.position}\nRotation: {qrCode.transform.rotation}\nCenter {qrCode.center}\n";
            s += $"Text: {qrCode.GetDecodedString()}\n";
            s += $"State: {qrCode.trackingState}";
            debugText.text = s;
        } 
    }
}

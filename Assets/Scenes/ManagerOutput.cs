using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.OpenXR;
using TMPro;

public class ManagerOutput : MonoBehaviour
{

    private ARMarkerManager m_arMarkerManager;
    public string outputString;
    public TextMeshProUGUI debugText;
    // Start is called before the first frame update
    void Start()
    {
        m_arMarkerManager = GetComponent<ARMarkerManager>();
        m_arMarkerManager.markersChanged += OnQRCodesChanged;
    }

    void OnQRCodesChanged(ARMarkersChangedEventArgs args)
    {
        foreach (ARMarker qrCode in args.added)
            outputString += $"QR code with the ID {qrCode.trackableId} added.";

        foreach (ARMarker qrCode in args.removed)
            outputString += $"QR code with the ID {qrCode.trackableId} removed.";

        foreach (ARMarker qrCode in args.updated)
        {
            outputString += $"QR code with the ID {qrCode.trackableId} updated.";
            outputString += $"Pos:{qrCode.transform.position} Rot:{qrCode.transform.rotation} Size:{qrCode.size}";
        }

        debugText.text = outputString;
    }
}

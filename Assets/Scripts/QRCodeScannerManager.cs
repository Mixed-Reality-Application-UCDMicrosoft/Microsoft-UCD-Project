using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.OpenXR;
using TMPro;
using UnityEngine;

public class QRCodeScannerManager : MonoBehaviour
{

    public ARMarkerManager arm;
    public TextMeshProUGUI debugText;
    public bool added = false;

    public GameObject skipButton;
    public GameObject nextButton;

    public GameObject defaultFloor;

    void Start()
    {
        arm.gameObject.SetActive(true);
        arm.markersChanged += OnQRCodesChanged;
        added = false;
        nextButton.SetActive(false);
        skipButton.SetActive(true);
    }


    void OnQRCodesChanged(ARMarkersChangedEventArgs args)
    {
        foreach (ARMarker qrCode in args.added)
        {
            string s = $"Position: {qrCode.transform.position}\nRotation: {qrCode.transform.rotation}\nCenter {qrCode.center}\n";
            s += $"Text: {qrCode.GetDecodedString()}\n";
            s += $"State: {qrCode.trackingState}";
            debugText.text = s  + "\n" + "QR Code Added";
            added = true;
            skipButton.SetActive(false);
            nextButton.SetActive(true);
        }

        foreach (ARMarker qrCode in args.updated)
        {
            string s = $"Position: {qrCode.transform.position}\nRotation: {qrCode.transform.rotation}\nCenter {qrCode.center}\n";
            s += $"Text: {qrCode.GetDecodedString()}\n";
            s += $"State: {qrCode.trackingState}";
            debugText.text = s + "\n" + "QR Code Modified";
        }
    }

    public void OnSkip()
    {
        debugText.text = "Added default floor";
        defaultFloor.SetActive(true);
        nextButton.SetActive(true);
        skipButton.SetActive(false);
        added = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.OpenXR;
using TMPro;
using UnityEngine;

public class MarkerOutputManager : MonoBehaviour
{
    [SerializeField] private NetworkManagerEvent networkManager;
    [SerializeField] private GameObject cameraObject;

    [SerializeField] private bool simulateSending = false;
    [SerializeField] private TextMeshProUGUI debugText;

    private ARMarkerManager arm;

    public Vector3 CoOrds;
    
    private void Start()
    {
        arm = GetComponent<ARMarkerManager>();
        arm.markersChanged += OnQRCodesChanged;        
    }

    private void OnQRCodesChanged(ARMarkersChangedEventArgs args)
    {
        foreach (ARMarker code in args.updated)
        {
            Vector3 pos = code.transform.position;
            networkManager.SendPlayerQRCodePos(pos);
            CoOrds = pos;

            string s = $"Position: {code.transform.position}\nRotation: {code.transform.rotation}\nCenter {code.center}\n";
            s += $"Text: {code.GetDecodedString()}\n";
            s += $"State: {code.trackingState}";
            debugText.text = s;
        }
    }
    private void Update()
    {
        if(networkManager.isNetworkActive && simulateSending)
        {
            networkManager.SendPlayerQRCodePos(new Vector3(5,5,5));
            CoOrds = new Vector3(5, 5, 5);
            simulateSending = false;
        }
    }
}

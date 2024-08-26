using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;
using TMPro;
using MixedReality.Toolkit.SpatialManipulation;

public class WeddingChange : MonoBehaviour
{
    public NetworkedObjectManipulator currentlySelected;

    [HideInInspector] public ColorChangeUIManager colorChangeUI;
    [HideInInspector] public OptionsManager optionsUI;

    private void Start()
    {
        colorChangeUI = FindObjectOfType<ColorChangeUIManager>();
        optionsUI = FindObjectOfType<OptionsManager>();
    }

    public void OnInteraction(GameObject interactedObject)
    {
        currentlySelected = interactedObject.GetComponent<NetworkedObjectManipulator>();
        colorChangeUI.selectedObject = currentlySelected;
        optionsUI.selectedObject = currentlySelected;
        optionsUI.UpdateUI();
    }

    public void OnCancelDialogue()
    {
        currentlySelected = null;
        colorChangeUI.selectedObject = null;
        optionsUI.selectedObject = null;
    }
}

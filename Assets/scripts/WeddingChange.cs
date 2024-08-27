using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;
using TMPro;

public class WeddingChange : MonoBehaviour
{
    public NetworkedObjectManipulator currentlySelected;
    public GameObject NonNetworkedObjects;

    [HideInInspector] public ColorChangeUIManager colorChangeUI;
    [HideInInspector] public OptionsManager optionsUI;

    private void Start()
    {
        if (NonNetworkedObjects != null)
            Instantiate(NonNetworkedObjects, transform.parent.parent);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;
using TMPro;
using MixedReality.Toolkit.SpatialManipulation;

public class WeddingChange : MonoBehaviour
{
    public NetworkedObjectManipulator currentlySelected;
    public TextMeshProUGUI colorText;
    public TextMeshProUGUI selectedHeader;

    public Slider r, g, b;

    public GameObject chairObject;
    public GameObject chairParent;

    public ColorChangeUIManager colorChangeUI;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        colorText.text = "█";

        colorChangeUI = FindObjectOfType<ColorChangeUIManager>();
        Debug.Log(colorChangeUI.name);

    }

    public void OnInteraction(GameObject interactedObject)
    {
        interactedObject.GetComponent<BoundsControl>().HandlesActive = true;


        currentlySelected = interactedObject.GetComponent<NetworkedObjectManipulator>();
        colorChangeUI.selectedObject = currentlySelected;
    }

    public void OnColorChange(SelectedInformation selected)
    {
        Color color = new Color();
        color.a = 1;
        Slider slider = selected.selectedObject.GetComponent<Slider>();
        if (selected.selectedString == "RED")
        {
            color.g = colorText.color.g;
            color.b = colorText.color.b;
            color.r = slider.Value;
        } else if (selected.selectedString == "BLUE")
        {
            color.g = colorText.color.g;
            color.r = colorText.color.r;
            color.b = slider.Value;

        } else if (selected.selectedString == "GREEN")
        {
            color.r = colorText.color.r;
            color.b = colorText.color.b;
            color.g = slider.Value;
        } else
        {
            Debug.LogError("Color value not found: " + selected.selectedString);
        }
        colorText.color = color;
    }

    public void OnCancelDialogue()
    {
        var bounds = currentlySelected.GetComponent<BoundsControl>();

        currentlySelected = null;
        colorChangeUI.selectedObject = null;
    }

    public void OnApplyAll()
    {
        
    }

    public void OnConfirm()
    {
    }
}

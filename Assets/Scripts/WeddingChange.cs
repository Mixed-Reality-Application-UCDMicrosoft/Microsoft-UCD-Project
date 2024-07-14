using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;
using TMPro;
using MixedReality.Toolkit.SpatialManipulation;

public class WeddingChange : MonoBehaviour
{
    public ModifyableObject currentlySelected;
    public GameObject canvasDialogue;
    public TextMeshProUGUI colorText;
    public string str;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
        colorText.text = "█";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction(GameObject something)
    {
        currentlySelected = something.GetComponent<ModifyableObject>();
        Debug.Log("Interacted with " + something.name);
        canvasDialogue.SetActive(true);

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
        currentlySelected.ChangeColor(color);
    }

    public void OnCancelDialogue()
    {
        var bounds = currentlySelected.GetComponent<BoundsControl>();

        currentlySelected.ResetColor();
        currentlySelected = null;
        canvasDialogue.SetActive(false);
        Debug.Log("Closed dialogue");
    }

    public void OnApplyAll()
    {
        currentlySelected.ApplyCurrentColor();

        var c = currentlySelected.currentColor;
        var parent = currentlySelected.transform.parent;

        foreach (var child in parent.GetComponentsInChildren<ModifyableObject>())
        {
            child.currentColor = c;
            child.ChangeColor(c);
            child.ApplyCurrentColor();
        }
        
    }
}

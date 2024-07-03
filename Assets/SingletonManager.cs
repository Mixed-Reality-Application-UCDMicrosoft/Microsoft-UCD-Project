using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{

    public TextMeshPro currentText;
    public GameObject currentlySelected;

    private Transform currentlySelected_Interaction;

    [Header("UI Elements")]
    public Slider redSlider;
    public Slider blueSlider;
    public Slider greenSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentlySelected = null;
        currentText.text = "Currently Selected: None";
    }

    public void PrintSomething(string something)
    {
        Debug.Log("Print Something: " + something);
    }

    public void OnInteraction (string name) {
        Debug.Log(name);
        currentText.text = "Currently Selected: " + name;
        currentlySelected = GameObject.Find(name);
        currentlySelected_Interaction = currentlySelected.transform.Find("Table Detailed");

    }

    public void OnInteractionExit (string name) {
        Debug.Log(name + " Exited");
    }

    public void ChangeRed()
    {
        
        if (currentlySelected is not null) {
            Renderer r = currentlySelected_Interaction.GetComponent<Renderer>();
            var g = r.material.color.g;
            var b = r.material.color.b;
            r.material.color = new Color(redSlider.Value,g,b);
        }
    }

    public void ChangeBlue()
    {

        if (currentlySelected is not null)
        {
            Renderer render = currentlySelected_Interaction.GetComponent<Renderer>();
            var g = render.material.color.g;
            var r = render.material.color.r;
            render.material.color = new Color(r, g, blueSlider.Value);
        }
    }

    public void ChangeGreen()
    {

        if (currentlySelected is not null)
        {
            Renderer render = currentlySelected_Interaction.GetComponent<Renderer>();
            var r = render.material.color.r;
            var b = render.material.color.b;
            render.material.color = new Color(r, greenSlider.Value, b);
        }
    }
}

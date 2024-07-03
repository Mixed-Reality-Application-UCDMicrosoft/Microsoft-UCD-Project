using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Material[] targetMaterials;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    void Start()
    {
        redSlider.onValueChanged.AddListener(delegate { OnColorChanged(); });
        greenSlider.onValueChanged.AddListener(delegate { OnColorChanged(); });
        blueSlider.onValueChanged.AddListener(delegate { OnColorChanged(); });

        OnColorChanged();
    }

    public void OnColorChanged()
    {
        float red = redSlider.value;
        float green = greenSlider.value;
        float blue = blueSlider.value;

        Color newColor = new Color(red, green, blue);

        foreach (Material mat in targetMaterials)
        {
            mat.color = newColor;
        }
    }
}

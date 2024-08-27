using System;
using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using UnityEngine;

public class ColorChangeUIManager : MonoBehaviour
{
    [SerializeReference] private Slider redSlider;
    [SerializeReference] private Slider greenSlider;
    [SerializeReference] private Slider blueSlider;

    [Header("Debug do not assign")]
    public NetworkedObjectManipulator selectedObject = null;

    private int prevRed = 0;
    private int prevGreen = 0;
    private int prevBlue = 0;

    public void ChangeColor(string colorString)
    {
        if (selectedObject == null) return;
        int red = int.Parse(colorString.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
        int green = int.Parse(colorString.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
        int blue = int.Parse(colorString.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

        prevRed = red;
        prevGreen = green;
        prevBlue = blue;

        redSlider.Value = red;
        greenSlider.Value = green;
        blueSlider.Value = blue;
        Debug.Log($"Printing {colorString}");
        selectedObject.ChangeColor(colorString);
    }

    public void ChangeColorSlider()
    {
        if (selectedObject == null) return;

        int red = (int)Math.Round(redSlider.Value, 0);
        int green = (int)Math.Round(greenSlider.Value, 0);
        int blue = (int)Math.Round(blueSlider.Value, 0);

        if (prevRed == red && prevGreen == green && prevBlue == blue) return;

        selectedObject.ChangeColor($"#{red:X2}{green:X2}{blue:X2}");
    }

    public void OnApplyAll()
    {
        if (selectedObject == null) return;
        selectedObject.ApplyAll();
    }
}

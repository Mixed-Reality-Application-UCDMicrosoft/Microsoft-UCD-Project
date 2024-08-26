using System.Collections;
using System.Collections.Generic;
using MixedReality.Toolkit.UX;
using TMPro;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameLabel;
    [SerializeField] private Slider itemsSlider;


    [HideInInspector] public NetworkedObjectManipulator selectedObject = null;


    public void UpdateUI()
    {
        NameLabel.text = selectedObject.nameObject;

    }
}

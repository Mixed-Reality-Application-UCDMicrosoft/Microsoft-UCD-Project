using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeUIManager : MonoBehaviour
{
    public NetworkedObjectManipulator selectedObject = null;

    public void ChangeColor(string colorString)
    {
            selectedObject.ChangeColor(colorString);
    }
}

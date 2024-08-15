using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyableObject : MonoBehaviour
{
    public GameObject[] colorChangeObjects;
    public string nameObject;

    public Color currentColor;
    

    public void ChangeColor(Color c)
    {
        for (int i = 0; i < colorChangeObjects.Length; i++)
        {
            colorChangeObjects[i].GetComponent<Renderer>().material.color = c;
        }
    }

    public void ApplyCurrentColor()
    {
        currentColor = colorChangeObjects[0].GetComponent<Renderer>().material.color;
    }

    public void ResetColor()
    {
        for (int i = 0;i < colorChangeObjects.Length;i++)
        {
            colorChangeObjects[i].GetComponent<Renderer>();
        }
    }

    public Color GetColor() { return currentColor; }
}

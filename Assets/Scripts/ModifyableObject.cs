using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyableObject : MonoBehaviour
{
    public GameObject[] colorChangeObjects;
    public string nameObject;

    public Color currentColor;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void ChangeColor(Color c)
    { 
        for (int i = 0; i < colorChangeObjects.Length; i++) {
            colorChangeObjects[i].GetComponent<Renderer>().material.color = c;
        }
    }

    public void ApplyCurrentColor()
    {
        currentColor = colorChangeObjects[0].GetComponent<Renderer>().material.color;
    }

    public void ResetColor()
    {
        for (int i = 0; i < colorChangeObjects.Length; i++)
        {
            colorChangeObjects[i].GetComponent<Renderer>().material.color = currentColor;
        }
    }

    public Color GetColor()
    {
        return currentColor;
    }

    public void InteractionEnded()
    {
        rb.isKinematic = false;
    }

    public void InteractionStarted()
    {
        rb.isKinematic = true;
    }
}

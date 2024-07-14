using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyableObject : MonoBehaviour
{
    public GameObject colorChangeObject;

    private Renderer r;
    public Color currentColor;

    private void Start()
    {
        r = colorChangeObject.GetComponent<Renderer>();
    }

    public void ChangeColor(Color c)
    {
        r.material.color = c;
    }

    public void ApplyCurrentColor()
    {
        currentColor = r.material.color;
    }

    public void ResetColor()
    {
        r.material.color = currentColor;
    }
}

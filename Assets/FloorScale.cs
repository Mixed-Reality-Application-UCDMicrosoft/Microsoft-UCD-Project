using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScale : MonoBehaviour
{
    public Vector3 WeddingScale = Vector3.one;
    public Vector3 CorporateScale = Vector3.one;

    public Layouts currentLayout;

    public void Start()
    {
        switch (currentLayout)
        {
            case Layouts.WEDDING1:
                transform.localScale = WeddingScale;
                transform.GetChild(0).localPosition -= new Vector3(0, 0,0.25f);
                break;
            case Layouts.CORPORATE1:
                transform.localScale = CorporateScale;
                transform.GetChild(0).localPosition -= new Vector3(0, 0, 0);
                break;
            default:
                return;
        }
    }
}

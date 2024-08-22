using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardAndBack : MonoBehaviour
{
    [SerializeField] private GameObject[] rows;
    private int currentPos;

    private void OnEnable()
    {
        currentPos = 0;
        foreach (var row in rows) row.SetActive(false);
        rows[currentPos].SetActive(true);
    }
    public void ScrollUp()
    {
        foreach (var r in rows)
            r.SetActive(false);
        if (currentPos > 0)
            currentPos--;
        rows[currentPos].SetActive(true);
    }

    public void ScrollDown()
    {
        foreach (var r in rows)
            r.SetActive(false);
        if (currentPos < rows.Length - 1)
            currentPos++;
        rows[currentPos].SetActive(true);
    }


}

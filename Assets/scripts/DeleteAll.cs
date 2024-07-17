using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAll : MonoBehaviour
{
    public GameObject[] ListOfObjects;
    public int listIdx;

    public void DeleteObjects()
    {
        for (int i = 0; i < listIdx; i++)
        {
            Destroy(ListOfObjects[i]);
        }

        listIdx = 0;
    }
}

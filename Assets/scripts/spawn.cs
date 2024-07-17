using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class spawn : MonoBehaviour
{

    public GameObject toSpawm = null;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (toSpawm != null)
            {
                Instantiate(toSpawm, transform.position, Quaternion.identity);
                yield return null;
            }
        }
    }
}

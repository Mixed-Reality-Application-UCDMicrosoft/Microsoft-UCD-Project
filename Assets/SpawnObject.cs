using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject o;
    public Vector3 position;
    public Vector3 scale;

    public void Spawn()
    {
        Instantiate(o, position, Quaternion.identity);
    }
}

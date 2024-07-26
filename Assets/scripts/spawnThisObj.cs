using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;

public class spawnThisObj : MonoBehaviour
{
    public GameObject Obj_toSpawn; 
    public Transform plane; 
    private GameObject theCamera, HouseMenu;

    public GameObject[] LofO; 
    public int Idx = 0; 

    void Start()
    {

        theCamera = GameObject.Find("Main Camera");
        HouseMenu = GameObject.Find("HandMenu");

        
        LofO = new GameObject[10]; 
    }

    public void Spawner()
    {

        Vector3 spawnPosition = plane.position + plane.up + theCamera.transform.forward * 2;
        GameObject objectSpawned = Instantiate(Obj_toSpawn, spawnPosition, Quaternion.identity);
        
        
        if (Idx < LofO.Length)
        {
            LofO[Idx] = objectSpawned;
            Idx++;
        }
        else
        {
            Debug.LogWarning("LofO array is full!");
        }

        
    }
}


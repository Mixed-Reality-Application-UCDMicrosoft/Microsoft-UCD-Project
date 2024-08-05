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
        HouseMenu = GameObject.Find("VerticalScrolling");

        
        LofO = new GameObject[10]; 
    }

    public void Spawner()
    {
        float spawnHeightOffset = 0.01f;
        float cameraOffset = -0.5f;

        Vector3 planeTopPosition = plane.position + plane.up * (plane.localScale.y / 2 + spawnHeightOffset);
        Vector3 cameraPosition = theCamera.transform.position + theCamera.transform.forward * cameraOffset;

        Vector3 spawnPosition = new Vector3(cameraPosition.x, planeTopPosition.y, cameraPosition.z);

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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MixedReality.Toolkit.UX;

public class spawnThisObj : MonoBehaviour
{
    public GameObject Obj_toSpawn;
    private GameObject theCamera, HouseMenu;

    public GameObject[] LofO;
    public int Idx = 0;

    // Height offset to spawn the object just above the floor, 0.05m should be enough.
    public float spawnHeightOffset = 0.05f;

    // Distance in meters to spawn the object in front of the camera
    public float cameraForwardOffset = 1.5f;

    void Start()
    {
        theCamera = GameObject.Find("Main Camera");
        HouseMenu = GameObject.Find("VerticalScrolling");

        LofO = new GameObject[10];
    }

    public void Spawner()
    {
        // Define ray origin to be a bit in front of the camera
        Vector3 rayOrigin = theCamera.transform.position + theCamera.transform.forward * cameraForwardOffset;

        // Raycast downward from the rayOrigin to detect the floor
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit))
        {
            // Position the object just above the floor (detected by the raycast)
            Vector3 floorPosition = hit.point;
            Vector3 spawnPosition = new Vector3(floorPosition.x, floorPosition.y + spawnHeightOffset, floorPosition.z);

            // Instantiate the object at the calculated position
            GameObject objectSpawned = Instantiate(Obj_toSpawn, spawnPosition, Quaternion.identity);

            // Store the spawned object in the array if there is space
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
        else
        {
            Debug.LogWarning("No floor detected! Ensure the floor has a collider.");
        }
    }
}

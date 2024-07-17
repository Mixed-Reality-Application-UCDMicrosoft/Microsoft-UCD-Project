using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// the Obj_toSpam have to be a prefab GameObject
public class spawnThisObj : MonoBehaviour
{
    //public Transform toSpawm = null;
    public GameObject Obj_toSpawn;
    public Transform plane;
    GameObject theCamera, HouseMenu;

    public GameObject[] LofO;
    public int Idx;
    

    void Start()
    {
        theCamera = GameObject.Find("Main Camera");
        HouseMenu = GameObject.Find("PalmUpHouseMenu");

        LofO = HouseMenu.GetComponent<DeleteAll>().ListOfObjects;
    }

    public void Spawner()
    {
        // add the component to each of the instantiated objects without the need to have the original one in the scene
        // catch the object that has been instantiated
        //GameObject objectSpawned = Instantiate(Obj_toSpawn, transform.position + plane.up + theCamera.transform.forward * 2, Quaternion.identity);
        GameObject objectSpawned = Instantiate(Obj_toSpawn);


        Idx = HouseMenu.GetComponent<DeleteAll>().listIdx;
        LofO[Idx] = objectSpawned;

        HouseMenu.GetComponent<DeleteAll>().listIdx++;
    }
}

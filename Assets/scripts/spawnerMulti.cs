using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerMulti : MonoBehaviour
{
    //public Transform toSpawm = null;
    public GameObject[] Obj_toSpawn;        // list of GameObjects
    public float rate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (Obj_toSpawn != null)
            {
                // (object to spawn, position, rotation)
                //Instantiate(toSpawm, transform.position + Random.insideUnitSphere, Quaternion.identity);
                //Instantiate(toSpawm, transform.position + Vector3.forward, Quaternion.identity);

                System.Random randGen = new System.Random();
                int randNum = randGen.Next(Obj_toSpawn.Length);

                // add the component to each of the instantiated objects without the need to have the original one in the scene
                // catch the object that has been instantiated
                GameObject objectSpawned = Instantiate(Obj_toSpawn[randNum], transform.position + transform.forward, Quaternion.identity);
                // add a component 
                objectSpawned.AddComponent<pushForward>();

                yield return new WaitForSeconds(1f / rate);       // rate = 1   ->  
                // spawm guaranteed for a fixed rate (not every frame)
            }
        }
    }
}

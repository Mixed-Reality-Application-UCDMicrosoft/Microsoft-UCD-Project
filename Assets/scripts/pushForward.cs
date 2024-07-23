using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this script is being called from the spawner.cs (line 32) instead of by an object
public class pushForward : MonoBehaviour
{
    public float push = 100;
    //public Transform gun;
    public GameObject gun;

    public void Start()
    {
        gun = GameObject.Find("SciFiGunLightBlack");

        Rigidbody rbComp = GetComponent<Rigidbody>();
        //Transform transComp = GetComponent<Transform>();
        // GetComponent<...>    ->  get components from the gameObject

        rbComp.mass = .1f;
        rbComp.AddForce(gun.transform.forward * push);
    }

}

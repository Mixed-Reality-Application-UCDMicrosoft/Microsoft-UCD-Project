using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bumpUp : MonoBehaviour
{
    public float push = 400;
    public Transform plane;
    public string objectRotation = "";

    // reaction to Collision event
    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbComp = GetComponent<Rigidbody>();
        Transform transComp = GetComponent<Transform>();
        // GetComponent<...>    ->  get components from the gameObject
        
        //rbComp.AddForce(transform.up * push);
        rbComp.AddForce(plane.up * push);       // "up" respect the plane transform
        // use the RigidBody instance to push the gameObject "up"

        Debug.Log("bounce");
        objectRotation = Mathf.Round(transComp.eulerAngles.y).ToString();
        // Debug.Log("rotation: " + objectRotation);
    }
}

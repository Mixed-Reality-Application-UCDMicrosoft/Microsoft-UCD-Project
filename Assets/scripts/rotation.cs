using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotation : MonoBehaviour
{
    public float speed = 5f;
    bool turning = true;   

    void OnEnable()
    //void OnMouseDown()      // clicking on the object containing the script
    {
        //turning = !turning;

        StartCoroutine(Rotate());
    }

    /*
    void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime);
        // transform.up     ->  axis in which the rotation is applied
        // Time.deltaTime   ->  make rotation time-dependent and frame-independent
        // (more time efficient, avoid delays if having low computation frame rate)
    }
    */


    IEnumerator Rotate()
    {
        while (true)
        {
            if (turning == true)
                transform.Rotate(transform.up, speed / 10);
                
            yield return null;      // gives the control to the next frame
        }
    }

}

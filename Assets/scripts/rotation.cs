using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotation : MonoBehaviour
{
    public float speed = 5f;
    bool turning = true;   


    
    void FixedUpdate()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime);
    }
    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1. the script is loaded into the object to make (in)visible
// 2. the method 'ToggleVisibility' is called by an external event (pressing the button) 
// 3. to connect the button event go to the button root
//      Button Config Helper -> Basic Events -> connect the object (drag & drop)
// 4. select a function to the object
//  * (the scrip has to be a component in the object)
//      funtion -> script name -> method name
public class visibility : MonoBehaviour
{
    public void ToggleVisible()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        bool visibility = meshRenderer.enabled;

        meshRenderer.enabled = !visibility; 
    }

}

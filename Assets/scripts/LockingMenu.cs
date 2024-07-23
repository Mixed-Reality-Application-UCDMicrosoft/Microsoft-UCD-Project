using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockingMenu : MonoBehaviour
{
    public void Lock()
    {
        Component IsLocked = GetComponent("HandConstraintPalmUp");
        Behaviour bhvr = (Behaviour)IsLocked;
        bhvr.enabled = !bhvr.enabled;

        GameObject FollowCont = GameObject.Find("FollowContainer");
        if (bhvr.enabled == true)
        {
            FollowCont.transform.localPosition = new Vector3(0, 0, 0);
            FollowCont.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

}

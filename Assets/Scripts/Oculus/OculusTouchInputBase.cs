using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class OculusTouchInputBase : MonoBehaviour
{
    protected OVRInput.Controller thisController = OVRInput.Controller.None;

    protected virtual void Awake()
    {
        string lowerCaseName = name.ToLower();

        if ( lowerCaseName.Contains( "left" ) )
        {
            thisController = OVRInput.Controller.LTouch;
        }
        else if ( lowerCaseName.Contains( "right" ) )
        {
            thisController = OVRInput.Controller.RTouch;
        }

        Assert.IsFalse( thisController == OVRInput.Controller.None, "Unable to determine if the OculusTouchInputBase is attached to the left or right hand" );
    }
}

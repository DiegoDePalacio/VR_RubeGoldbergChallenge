using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.VR;

public class HeadsetManager : MonoBehaviour
{
    [SerializeField] private GameObject viveRig;
    [SerializeField] private GameObject oculusRig;

    private bool hmdChosen = false;

    private void Awake()
    {
        Assert.IsNotNull( viveRig, "The Vive rig wasn't found!" );
        Assert.IsNotNull( oculusRig, "The Oculus rig wasn't found!" );

        SetHMD();
    }

	// Update is called once per frame
	void Update ()
    {
        if ( !hmdChosen )
        {
            SetHMD();
        }

        if ( !VRDevice.isPresent )
        {
            hmdChosen = false;
            // Do code to sync up the CameraRigs states
            // such as position here
        }
	}

    private void SetHMD()
    {
        string lowerCaseModelName = VRDevice.model.ToLower();

        if ( lowerCaseModelName.Contains( "vive" ) )
        {
            viveRig.SetActive( true );
            oculusRig.SetActive( false );
            hmdChosen = true;
        }
        else if ( lowerCaseModelName.Contains( "oculus" ) )
        {
            oculusRig.SetActive( true );
            viveRig.SetActive( false );
            hmdChosen = true;
        }
        else
        {
            Debug.LogWarning( "No HDM found!" );
        }
    }
}

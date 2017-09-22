using Common.PropertyDrawers;
using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Oculus
{
    public class OculusThumbstickRayCaster : OculusTouchInputBase
    {
        [SerializeField] private RayCaster rayCaster;

        protected override void Awake()
        {
            base.Awake();
            rayCaster.Init();
        }

	    void Update ()
        {
            if ( OVRInput.Get( OVRInput.Button.PrimaryThumbstick, thisController ) )
            {
                rayCaster.CastRay( transform.position, transform.forward );
            }

            if ( OVRInput.GetUp( OVRInput.Button.PrimaryThumbstick, thisController ) )
            {
                rayCaster.CastRaySubmission();
            }
	    }
    }
}

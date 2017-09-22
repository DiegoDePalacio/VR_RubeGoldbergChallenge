using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VirtualReality.Oculus
{
    public class OculusOnHandTriggerSendBool : OculusTouchInputBase
    {
        [SerializeField] private BoolCaster boolCaster;
        [SerializeField] private bool boolToSend;

        protected override void Awake()
        {
            base.Awake();
            boolCaster.Init();
        }
	
	    // Update is called once per frame
	    void Update ()
        {
            if ( OVRInput.GetUp( OVRInput.Button.PrimaryHandTrigger, thisController ) )
            {
                boolCaster.CastBool( boolToSend );
            }
	    }
    }
}

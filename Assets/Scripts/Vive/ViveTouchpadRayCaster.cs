using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Vive
{
    public class ViveTouchpadRayCaster : ViveInputBase
    {
        [SerializeField] private RayCaster rayCaster;

        protected override void Awake()
        {
            base.Awake();
            rayCaster.Init();
        }

        void Update()
        {
            if ( device.GetPress( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
            {
                rayCaster.CastRay( transform.position, transform.forward );
            }

            if ( device.GetPressUp( Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad ) )
            {
                rayCaster.CastRaySubmission();
            }
        }
    }
}

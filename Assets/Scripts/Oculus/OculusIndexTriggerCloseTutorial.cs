using Common.Communication;
using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Oculus
{
    // One time use trigger for being used once and destroyed just after its usage
    public class OculusIndexTriggerCloseTutorial : OculusTouchInputBase
    {
        [SerializeField] private VoidCaster closeTutorial;

        protected override void Awake()
        {
            base.Awake();
            closeTutorial.Init();
        }

        private void Update()
        {
            if ( OVRInput.GetUp( OVRInput.Button.PrimaryIndexTrigger, thisController ) )
            {
                closeTutorial.Notify();
                Destroy( this );
            }
        }
    }
}

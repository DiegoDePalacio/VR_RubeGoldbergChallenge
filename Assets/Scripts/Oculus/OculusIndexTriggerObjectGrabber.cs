using Common.Communication;
using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Oculus
{
    public class OculusIndexTriggerObjectGrabber : OculusTouchInputBase
    {
        [SerializeField] [TagSelector] private string grabbableTag = string.Empty;
        [SerializeField] private GrabObjectCaster grabObjectCaster;

        private Collider grabbedObject = null;

        protected override void Awake()
        {
            base.Awake();
            grabObjectCaster.Init();

            Assert.IsFalse( grabbableTag == string.Empty, "The grabbable tag wasn't set!" );
        }

        private void OnTriggerStay( Collider other )
        {
            if ( other.CompareTag( grabbableTag ) )
            {
                if ( grabbedObject == other && OVRInput.GetUp( OVRInput.Button.PrimaryIndexTrigger, thisController ) )
                {
                    Debug.Log( "GetUp Touch trigger" );
                    Vector3 velocity = OVRInput.GetLocalControllerVelocity( thisController );
                    Vector3 angularVelocity = OVRInput.GetLocalControllerAngularVelocity( thisController );
                    grabObjectCaster.ReleaseObject( other.gameObject, velocity, angularVelocity );
                }
                if ( grabbedObject == null && OVRInput.GetDown( OVRInput.Button.PrimaryIndexTrigger, thisController ) )
                {
                    grabObjectCaster.GrabObject( other.gameObject );
                    grabbedObject = other;
                }

            }
        }

        private void OnTriggerExit( Collider other )
        {
            if ( grabbedObject == other )
            {
                grabbedObject = null;
            }
        }
    }
}

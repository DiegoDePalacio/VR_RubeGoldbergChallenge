using Common.Communication;
using Common.PropertyDrawers;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Vive
{
    public class ViveTriggerObjectGrabber : ViveInputBase
    {
        [SerializeField] [TagSelector] private string grabbableTag = string.Empty;
        [SerializeField] private GrabObjectCaster grabObjectCaster;

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
                if ( device.GetPress( SteamVR_Controller.ButtonMask.Trigger ) )
                {
                    grabObjectCaster.GrabObject( other.gameObject );
                }

                if ( device.GetPressUp( SteamVR_Controller.ButtonMask.Trigger ) )
                {
                    grabObjectCaster.ReleaseObject( other.gameObject, device.velocity, device.angularVelocity );
                }
            }
        }
    }
}
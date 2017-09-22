using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Assertions;

namespace Common.Communication
{
    public class CameraFacingBillboard : MonoBehaviour, IGameObjectReceiver
    {
        private Camera cameraToAlign = null;

        public Action<GameObject> OnGameObject { get { return SetCamera; } }

#region [IGameObjectReceiver]
        private void SetCamera( GameObject newCameraToAlign )
        {
            cameraToAlign = newCameraToAlign.GetComponent<Camera>();

            Assert.IsNotNull( cameraToAlign, string.Format( "{0} doesn't have a Camera component attached to be linked to the CameraFacingBillboard of {1}", newCameraToAlign.name, name ) );
        }
#endregion

        private void Start()
        {
            // This should be received during the Awake phase
            Assert.IsNotNull( cameraToAlign, "No camera to be aligned was assigned for " + name );
        }

        void Update()
        {
            transform.LookAt( transform.position + cameraToAlign.transform.rotation * Vector3.forward,
                cameraToAlign.transform.rotation * Vector3.up );
        }
    }
}
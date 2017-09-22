using Common.Communication;
using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Vive
{
    public class ViveTriggerCloseTutorial : ViveInputBase
    {
        [SerializeField] private VoidCaster closeTutorial;

        protected override void Awake()
        {
            base.Awake();
            closeTutorial.Init();
        }

        private void Update()
        {
            if ( device.GetPressUp( SteamVR_Controller.ButtonMask.Trigger ) )
            {
                closeTutorial.Notify();
                Destroy( this );
            }
        }
    }
}

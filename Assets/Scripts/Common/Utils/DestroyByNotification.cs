using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public class DestroyByNotification : MonoBehaviour, IVoidReceiver
    {
        public Action OnNotified { get { return OnDestroyNotificationReceived; } }

        private void OnDestroyNotificationReceived()
        {
            // If the game object was not yet destroyed by someone else
            if ( !this.Equals( null ) )
            {
                Destroy( gameObject );
            }
        }
    }
}

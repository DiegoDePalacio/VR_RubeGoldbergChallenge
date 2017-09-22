using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Communication
{
    public class ParentReceiver : MonoBehaviour, IGameObjectReceiver
    {
        // Created only for error check... ToDo: Enclose it on a preprocessor region
        private GameObject parent;

        [SerializeField] bool resetPositionInParent = true;

        public Action<GameObject> OnGameObject { get { return SetParent; } }

        void Start ()
        {
            // This should be received during the Awake phase
            Assert.IsNotNull( parent, "No parent was assigned for " + name );
	    }
	
#region [IGameObjectReceiver]
        private void SetParent( GameObject newParent )
        {
            parent = newParent;
            transform.SetParent( newParent.transform );

            if ( resetPositionInParent )
            {
                transform.localPosition = Vector3.zero;
            }
        }
#endregion
    }
}

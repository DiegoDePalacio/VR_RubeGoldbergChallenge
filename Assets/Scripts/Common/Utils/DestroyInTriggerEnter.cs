using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Utils
{
    [RequireComponent(typeof(Collider))]
    public class DestroyInTriggerEnter : MonoBehaviour
    {
        [SerializeField] [TagSelector] private string destroyWithTag = string.Empty;

        private void Awake()
        {
            Assert.IsTrue( destroyWithTag != string.Empty, "Destroy tag was not set for " + name );

            Collider destroyableCollider = GetComponent<Collider>();
            Assert.IsNotNull( destroyableCollider, "There is no collider attached to the DestroyInCollision component of " + name );
        }

        private void OnTriggerEnter( Collider other )
        {
            if ( other.CompareTag( destroyWithTag ) )
            {
                Destroy( gameObject );
            }
        }
    }
}

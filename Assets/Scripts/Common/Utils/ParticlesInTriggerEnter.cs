using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Utils
{
    [RequireComponent( typeof( Collider ) )]
    public class ParticlesInTriggerEnter : MonoBehaviour
    {
        [SerializeField] [TagSelector] private string colliderTag = string.Empty;
        private ParticleSystem[] particles = null;

        private void Awake()
        {
            Assert.IsFalse( colliderTag == string.Empty, "The collider tag for particles wasn't set in " + name + "!" );

            particles = GetComponentsInChildren<ParticleSystem>();

            Assert.IsFalse( particles.Length == 0, "No particles assigned in " + name + "!" );
        }

        private void OnTriggerEnter( Collider collider )
        {
            if ( collider.gameObject.CompareTag( colliderTag ) )
            {
                foreach ( ParticleSystem particleSystem in particles )
                {
                    particleSystem.Clear();
                    particleSystem.transform.position = collider.transform.position;
                    particleSystem.Play();
                }
            }
        }
    }
}

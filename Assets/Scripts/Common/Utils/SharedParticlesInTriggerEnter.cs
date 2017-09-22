using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Utils
{
    [RequireComponent( typeof( Collider ) )]
    public class SharedParticlesInTriggerEnter : MonoBehaviour
    {
        [SerializeField] [TagSelector] private string colliderTag = string.Empty;

        [Tooltip("Itself if not set")]
        [SerializeField] private GameObject sharedParticlesHolder = null;
        private ParticleSystem[] sharedParticles = null;

        private void Awake()
        {
            Assert.IsFalse( colliderTag == string.Empty, "The collider tag for particles wasn't set in " + name + "!" );

            if ( sharedParticlesHolder == null )
            {
                sharedParticlesHolder = gameObject;
            }

            sharedParticles = sharedParticlesHolder.GetComponentsInChildren<ParticleSystem>();
            Assert.IsFalse( sharedParticles.Length == 0, "No particles assigned in " + name + "!" );
        }

        private void OnTriggerEnter( Collider collider )
        {
            if ( collider.gameObject.CompareTag( colliderTag ) )
            {
                sharedParticlesHolder.transform.position = collider.transform.position;

                foreach ( ParticleSystem particleSystem in sharedParticles )
                {
                    particleSystem.Clear();
                    particleSystem.Play();
                }
            }
        }
    }
}

using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Utils
{
    [RequireComponent(typeof(Collider))]
    public class ParticlesInCollision : MonoBehaviour
    {
        [SerializeField][TagSelector] private string colliderTag = string.Empty;

        [Tooltip("Itself if not set")]
        [SerializeField] private GameObject particleHolder = null;
        private ParticleSystem[] particles = null;

        private void Awake()
        {
            Assert.IsFalse( colliderTag == string.Empty, "The collider tag for particles wasn't set in " + name + "!" );

            if ( particleHolder == null )
            {
                particleHolder = gameObject;
            }

            particles = particleHolder.GetComponentsInChildren<ParticleSystem>();
            Assert.IsFalse( particles.Length == 0, "No particles assigned in " + name + "!" );
        }

        private void OnCollisionEnter( Collision collision )
        {
            if ( collision.gameObject.CompareTag( colliderTag ) )
            {
                particleHolder.transform.position = collision.contacts[0].point;

                foreach ( ParticleSystem particleSystem in particles )
                {
                    particleSystem.Clear();
                    particleSystem.Play();
                }
            }
        }
    }
}

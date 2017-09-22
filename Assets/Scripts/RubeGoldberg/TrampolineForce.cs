using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineForce : MonoBehaviour
{
    [SerializeField]
    private float velocityMultiplier = 2f;

    private void OnCollisionExit( Collision collision )
    {
        Rigidbody otherRigidbody = collision.collider.GetComponent<Rigidbody>();

        if ( otherRigidbody != null )
        {
            otherRigidbody.velocity *= velocityMultiplier;
        }
    }
}

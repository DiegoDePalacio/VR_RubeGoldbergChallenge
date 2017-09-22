using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForce : MonoBehaviour
{
    [SerializeField] private float forceMultiplier = 2f;

    private void OnTriggerStay( Collider other )
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();

        if ( otherRigidbody != null )
        {
            otherRigidbody.AddForce( transform.forward * forceMultiplier );
        }
    }
}

using Common.Communication;
using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System;

[RequireComponent(typeof(Rigidbody))]
public class BallReset : MonoBehaviour, IVoidReceiver
{
    [SerializeField][TagSelector] private string groundTag = string.Empty;
    [SerializeField] private VoidCaster resetLevel;

    private Vector3 initialPosition = Vector3.zero;
    private Rigidbody ballRigidBody = null;

    public Action OnNotified { get { return OnPlayerGoingToTheGround; } }

#region [IVoidReceiver]
    private void OnPlayerGoingToTheGround()
    {
        RestoreBall();
    }
#endregion

    private void Awake()
    {
        Assert.IsFalse( groundTag == string.Empty, "The ground tag wasn't set!" );

        initialPosition = transform.position;

        ballRigidBody = GetComponent<Rigidbody>();
        Assert.IsNotNull( ballRigidBody, "There is no rigidbody attached to the BallReset component!" );

        resetLevel.Init();
    }

    private void OnCollisionEnter( Collision collision )
    {
        if ( collision.gameObject.CompareTag( groundTag ) )
        {
            RestoreBall();
        }
    }

    private void RestoreBall()
    {
        transform.SetParent( null );
        transform.position = initialPosition;

        ballRigidBody.isKinematic = false;
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;

        // Notify the collectibles to be restored
        resetLevel.Notify();
    }
}

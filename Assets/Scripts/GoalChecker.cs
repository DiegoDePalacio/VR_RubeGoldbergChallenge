using Common.Communication;
using Common.PropertyDrawers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// All the children of this game object needs to be removed / destroyed before colliding with this game object in order to reach the goal
[RequireComponent(typeof(Collider))]
public class GoalChecker : MonoBehaviour
{
    [SerializeField] [TagSelector] private string colliderTag = string.Empty;
    [SerializeField] private BoolCaster levelOverCaster;

    private void Awake()
    {
        Assert.IsTrue( colliderTag != string.Empty, "No collider tag for the GoalChecker was set in " + name + "!" );

        levelOverCaster.Init();
    }

    private void OnCollisionEnter( Collision collision )
    {
        if ( collision.gameObject.CompareTag( colliderTag ) )
        {
            // The level is succesfully completed if there is no remaining child to be collected
            levelOverCaster.CastBool( transform.childCount == 0 );
        }
    }
}

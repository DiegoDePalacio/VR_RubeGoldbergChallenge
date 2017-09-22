using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Common.PropertyDrawers;
using UnityEngine.Assertions;

[RequireComponent( typeof( Collider ) )]
public class CollectibleHandler : MonoBehaviour, IVoidReceiver
{
    public Action OnNotified { get { return Restore; } }

    [SerializeField] [TagSelector] private string collectibleBy = string.Empty;

    [SerializeField] private GameObjectCaster ToReachContainer;
    [SerializeField] private GameObjectCaster ReachedContainer;

#region [IVoidReceiver]
    private void Restore()
    {
        // Change the parent to the collectibles to be collected
        ToReachContainer.CastGameObject( gameObject );
        gameObject.SetActive( true );
    }
#endregion

    private void Awake()
    {
        Assert.IsTrue( collectibleBy != string.Empty, "CollectibleBy tag was not set for " + name );

        Collider collectibleCollider = GetComponent<Collider>();
        Assert.IsNotNull( collectibleCollider, "There is no collider attached to the CollectibleHandler component of " + name );

        ToReachContainer.Init();
        ReachedContainer.Init();
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.CompareTag( collectibleBy ) )
        {
            // Change the parent to the collectibles already collected
            ReachedContainer.CastGameObject( gameObject );
            gameObject.SetActive( false );
        }
    }
}

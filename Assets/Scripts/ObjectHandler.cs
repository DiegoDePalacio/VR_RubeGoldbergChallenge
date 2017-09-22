using Common.Communication;
using UnityEngine;

public class ObjectHandler : MonoBehaviour, IGrabObjectReceiver
{
    [Tooltip("Leave the value in zero for non-throwable objects")]
    [SerializeField] private float throwSpeed = 1.5f;

    private GameObject objectGrabber = null;
    private Transform throwableRoot = null;

    public OnGrabObject OnGrabObject { get { return GrabThrowable; } }
    public OnReleaseObject OnReleaseObject { get { return ThrowThrowable; } }

#region [IGrabObjectReceiver]
    private void GrabThrowable( GameObject throwable )
    {
        // If something is already in the hand, wait first to be released
        if( throwableRoot != null ) return;

        throwableRoot = throwable.transform.root;
        throwableRoot.SetParent( transform );
        throwable.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ThrowThrowable( GameObject throwable, Vector3 velocity, Vector3 angularVelocity )
    {
        if( throwableRoot == null ) return;

        throwableRoot.SetParent( null );
        throwableRoot = null;

        // If the speed is zero, the object should be "placed" without any velocity at all
        if ( !Mathf.Approximately( throwSpeed, 0f ) )
        {
            Rigidbody throwableRigidbody = throwable.GetComponent<Rigidbody>();
            throwableRigidbody.isKinematic = false;
            throwableRigidbody.velocity = velocity * throwSpeed;
            throwableRigidbody.angularVelocity = angularVelocity;
        }
    }
#endregion
}
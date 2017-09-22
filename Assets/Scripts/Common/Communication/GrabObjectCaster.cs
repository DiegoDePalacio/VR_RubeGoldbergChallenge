using System;
using UnityEngine;

namespace Common.Communication
{
    public delegate void OnGrabObject( GameObject go );
    public delegate void OnReleaseObject( GameObject go, Vector3 velocity, Vector3 angularVelocity );

    public interface IGrabObjectReceiver
    {
        OnGrabObject OnGrabObject { get; }
        OnReleaseObject OnReleaseObject { get; }
    }

    [Serializable]
    public class GrabObjectCaster : Caster<IGrabObjectReceiver>
    {
        public OnGrabObject GrabObject = null;
        public OnReleaseObject ReleaseObject = null;

        public override void Init()
        {
            base.Init();

            foreach ( IGrabObjectReceiver receiver in receivers )
            {
                GrabObject -= receiver.OnGrabObject;
                GrabObject += receiver.OnGrabObject;

                ReleaseObject -= receiver.OnReleaseObject;
                ReleaseObject += receiver.OnReleaseObject;
            }
        }
    }
}

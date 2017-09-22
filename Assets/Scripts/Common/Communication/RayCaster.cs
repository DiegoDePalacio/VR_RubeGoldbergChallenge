using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public delegate void OnRayCast( Vector3 origin, Vector3 direction );
    public delegate void OnRaySubmit();

    public interface IRaycastReceiver
    {
        // To be called every frame, during the casting
        OnRayCast OnRayCast { get; }

        // To be called for confirming the last cast as the cast destination
        OnRaySubmit OnRaySubmit { get; }
    }

    [Serializable]
    public class RayCaster : Caster<IRaycastReceiver>
    {
        public OnRayCast CastRay = null;
        public OnRaySubmit CastRaySubmission = null;

        public override void Init()
        {
            base.Init();

            foreach ( IRaycastReceiver receiver in receivers )
            {
                CastRay -= receiver.OnRayCast;
                CastRay += receiver.OnRayCast;

                CastRaySubmission -= receiver.OnRaySubmit;
                CastRaySubmission += receiver.OnRaySubmit;
            }
        }
    }
}

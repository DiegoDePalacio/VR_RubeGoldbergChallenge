using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public interface ITransformReceiver
    {
        Action<Transform> OnTransform { get; }
    }

    [Serializable]
    public class TransformCaster : Caster<ITransformReceiver>
    {
        public Action<Transform> CastTransform = null;

        public override void Init()
        {
            base.Init();

            foreach ( ITransformReceiver receiver in receivers )
            {
                CastTransform -= receiver.OnTransform;
                CastTransform += receiver.OnTransform;
            }
        }
    }
}

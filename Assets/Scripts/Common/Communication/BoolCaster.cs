using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public interface IBoolReceiver
    {
        Action<bool> OnBool { get; }
    }

    [Serializable]
    public class BoolCaster : Caster<IBoolReceiver>
    {
        public Action<bool> CastBool = null;

        public override void Init()
        {
            base.Init();

            foreach ( IBoolReceiver receiver in receivers )
            {
                CastBool -= receiver.OnBool;
                CastBool += receiver.OnBool;
            }
        }
    }
}

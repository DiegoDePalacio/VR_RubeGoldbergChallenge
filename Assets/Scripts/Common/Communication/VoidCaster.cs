using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public interface IVoidReceiver
    {
        Action OnNotified { get; }
    }

    [Serializable]
    public class VoidCaster : Caster<IVoidReceiver>
    {
        public Action Notify = null;

        public override void Init()
        {
            base.Init();

            foreach ( IVoidReceiver receiver in receivers )
            {
                Notify -= receiver.OnNotified;
                Notify += receiver.OnNotified;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Communication
{
    public interface IGameObjectReceiver 
    {
        Action<GameObject> OnGameObject { get; }
    }

    [Serializable]
    public class GameObjectCaster : Caster<IGameObjectReceiver>
    {
        public Action<GameObject> CastGameObject = null;

        public override void Init()
        {
            base.Init();

            foreach ( IGameObjectReceiver receiver in receivers )
            {
                CastGameObject -= receiver.OnGameObject;
                CastGameObject += receiver.OnGameObject;
            }
        }
    }
}

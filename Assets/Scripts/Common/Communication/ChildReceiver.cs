using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Communication
{
    public class ChildReceiver : MonoBehaviour, IGameObjectReceiver
    {
        public Action<GameObject> OnGameObject { get { return SetParent; } }

#region [IGameObjectReceiver]
        private void SetParent( GameObject child )
        {
            child.transform.SetParent( transform, false );
        }
#endregion
    }
}

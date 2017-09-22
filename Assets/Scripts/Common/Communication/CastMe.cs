using Common.PropertyDrawers;
using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Communication
{
    public class CastMe : MonoBehaviour
    {
        [SerializeField]
        private GameObjectCaster gameObjectCaster;

        private void Awake()
        {
            gameObjectCaster.Init();
            gameObjectCaster.CastGameObject( gameObject );
        }
    }
}

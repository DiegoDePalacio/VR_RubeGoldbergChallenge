using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Utils
{
    public class SetCurrentLevelOnConfig : MonoBehaviour
    {
        [SerializeField] private int currentLevel = -1;

        private void Awake()
        {
            Assert.IsTrue( currentLevel >= 0, "The current level is set on the SetCurrentLevelOnConfig attached to " + name );
        }

        private void Start()
        {
            if ( currentLevel >= 0 )
            {
                Config.Global.CurrentLevel = currentLevel;
            }
        }
    }
}

using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VirtualReality.Vive
{
    public class ViveSceneLoader : MonoBehaviour, IBoolReceiver
    {
        public Action<bool> OnBool { get { return OnLevelComplete; } }

        private void OnLevelComplete( bool succesfully )
        {
            if ( succesfully )
            {
                int nextLevel = ( Config.Global.CurrentLevel + 1 ) % Config.Global.LEVELS.Length;
                SteamVR_LoadLevel.Begin( Config.Global.LEVELS[nextLevel] );
                Config.Global.CurrentLevel = nextLevel;
            }
        }
    }
}
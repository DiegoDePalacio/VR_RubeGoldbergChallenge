using Common.Communication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace VirtualReality.Oculus
{
    public class OculusSceneLoader : MonoBehaviour, IBoolReceiver
    {
        [SerializeField] private float fadeTime = 2f;
        [SerializeField] private Color fadeColor = new Color( 0f, 0f, 0f, 1f );

        private Material fadeMaterial = null;
        private bool isFading = false;
        private YieldInstruction fadeInstruction = new WaitForEndOfFrame();

        public Action<bool> OnBool { get { return OnLevelComplete; } }

#region [MonoBehaviour]
        private void Awake()
        {
            // create the fade material
            fadeMaterial = new Material( Shader.Find( "Oculus/Unlit Transparent Color" ) );
        }

        private void OnDestroy()
        {
            if ( fadeMaterial != null )
            {
                Destroy( fadeMaterial );
            }
        }

        private void OnPostRender()
        {
            if ( isFading )
            {
                fadeMaterial.SetPass( 0 );
                GL.PushMatrix();
                GL.LoadOrtho();
                GL.Color( fadeMaterial.color );
                GL.Begin( GL.QUADS );
                GL.Vertex3( 0f, 0f, -12f );
                GL.Vertex3( 0f, 1f, -12f );
                GL.Vertex3( 1f, 1f, -12f );
                GL.Vertex3( 1f, 0f, -12f );
                GL.End();
                GL.PopMatrix();
            }
        }
#endregion

#region [IBoolReceiver]
        private void OnLevelComplete( bool succesfully )
        {
            if ( succesfully )
            {
                StartCoroutine( FadeAndLoadTheNextLevel() );
            }
        }
#endregion

        private IEnumerator FadeAndLoadTheNextLevel()
        {
            float elapsedTime = 0.0f;
            fadeMaterial.color = fadeColor;
            Color color = fadeColor;
            isFading = true;

            while ( elapsedTime < fadeTime )
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01( elapsedTime / fadeTime );
                fadeMaterial.color = color;
                yield return fadeInstruction;
            }
            isFading = false;

            int nextLevel = ( Config.Global.CurrentLevel + 1 ) % Config.Global.LEVELS.Length;
            SceneManager.LoadScene( Config.Global.LEVELS[nextLevel] );
            Config.Global.CurrentLevel = nextLevel;
        }
    }
}

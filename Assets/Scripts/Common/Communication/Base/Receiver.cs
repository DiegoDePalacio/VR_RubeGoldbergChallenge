using Common.PropertyDrawers;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Communication
{
    public class Caster<T> where T : class
    {
        [SerializeField][TagSelector] private string receiverTag = string.Empty;

        protected T[] receivers = null;

	    public virtual void Init()
        {
            Assert.IsTrue( receiverTag != string.Empty, string.Format( "The receiver of {0} is not set!", typeof(T).FullName ) );
            if( receiverTag == string.Empty ) return;

            GameObject[] receiversGO = GameObject.FindGameObjectsWithTag( receiverTag );
            Assert.IsTrue( receiversGO.Length > 0, string.Format( "No receivers of {0} were found in the scene", typeof(T).FullName ) );
            if ( !( receiversGO.Length > 0 ) ) return;

            receivers = new T[receiversGO.Length];

            for ( int i = 0; i < receiversGO.Length; i++ )
            {
                receivers[i] = receiversGO[i].GetComponent<T>();
                Assert.IsNotNull( receivers[i], string.Format( "The GameObject {0} doesn't have a {1} attached", receiversGO[i].name, typeof(T).FullName ) );
            }
	    }
    }
}
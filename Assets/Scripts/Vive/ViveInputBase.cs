using UnityEngine;
using UnityEngine.Assertions;

namespace VirtualReality.Vive
{
    // Attach this script to any Vive object to be tracked, like controllers
    [RequireComponent( typeof( SteamVR_TrackedObject ) )]
    public class ViveInputBase : MonoBehaviour
    {
        protected SteamVR_TrackedObject trackedObj = null;
        protected SteamVR_Controller.Device device = null;

        protected virtual void Awake()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
            Assert.IsNotNull( trackedObj, "No SteamVR_TrackedObject is attached to " + name );
        }

        protected virtual void Start()
        {
            device = SteamVR_Controller.Input( ( int )( trackedObj.index ) );
        }
    }
}

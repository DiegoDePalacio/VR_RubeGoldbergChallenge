using Common.Communication;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using Common.PropertyDrawers;

public class TeleportManager : MonoBehaviour, IRaycastReceiver, IGameObjectReceiver
{
    // Teleport
    [SerializeField] private GameObject teleportAimer = null;
    [SerializeField] private LayerMask laserMask;

    [Tooltip("No tag if there is no special area to be notified")]
    [SerializeField] [TagSelector] private string specialAreaTag = string.Empty;
    [SerializeField] private VoidCaster teleportedToSpecialArea;

    private GameObject player = null;
    private Vector3 teleportLocation = Vector3.zero;
    private LineRenderer laser = null;
    private bool isTeleportedToSpecialArea = false;

    // Dash
    private bool isDashing;
    private float lerpTime;
    private Vector3 dashStartPosition;

    OnRayCast IRaycastReceiver.OnRayCast { get { return OnTeleportRayReceived; } }
    OnRaySubmit IRaycastReceiver.OnRaySubmit { get { return OnTeleportConfirmationReceived; } }

    Action<GameObject> IGameObjectReceiver.OnGameObject { get { return SetPlayer; } }

    private void Awake()
    {
        if ( specialAreaTag != string.Empty )
        {
            teleportedToSpecialArea.Init();
        }
        else
        {
            Debug.LogWarning( "No tag was set for a special area on the TeleportManager in " + name );
        }
    }

    private void Start()
    {
        laser = GetComponentInChildren<LineRenderer>();
        Assert.IsNotNull( laser, "No child of the TeleportManager with LineRenderer was found to draw the teleport ray" );
        laser.gameObject.SetActive( false );

        Assert.IsNotNull( teleportAimer, "The telepor aimer was not set!" );
        teleportAimer.SetActive( false );

        // The player GO should be received during the Awake phase
        Assert.IsNotNull( player, "The player is not assigned on the TeleportManager!" );
    }

    void Update ()
    {
        // Dashing
        if ( isDashing )
        {
            lerpTime += Time.deltaTime * Config.Global.DASH_SPEED;
            player.transform.position = Vector3.Lerp( dashStartPosition, teleportLocation, lerpTime );

            if ( lerpTime >= 1 )
            {
                isDashing = false;
                lerpTime = 0;
            }
            return;
        }
    }

#region [IGameObjectReceiver]
    public void SetPlayer( GameObject gameObject )
    {
        player = gameObject;
    }
#endregion

#region [IRaycastReceiver]
    public void OnTeleportRayReceived( Vector3 origin, Vector3 direction )
    {
        if( isDashing ) return;

        isTeleportedToSpecialArea = false;
        laser.gameObject.SetActive( true );
        teleportAimer.SetActive( true );

        laser.SetPosition( 0, origin );

        RaycastHit hit;
        if ( Physics.Raycast( origin, direction, out hit, Config.Global.MAX_TELEPORT_DISTANCE, laserMask ) )
        {
            teleportLocation = hit.point;
            laser.SetPosition( 1, teleportLocation );

            teleportAimer.transform.position = teleportLocation + Vector3.up * Config.Global.AIMER_Y_POS;

            if ( hit.transform.gameObject.CompareTag( specialAreaTag ) )
            {
                isTeleportedToSpecialArea = true;
            }
        }
        else
        {
            teleportLocation = origin + direction * Config.Global.MAX_TELEPORT_DISTANCE;

            laser.SetPosition( 1, teleportLocation );

            RaycastHit groundHit;
            if ( Physics.Raycast( teleportLocation, -Vector3.up, out groundHit, Config.Global.MAX_TELEPORT_RAY_HEIGHT, laserMask ) )
            {
                teleportLocation.y = groundHit.point.y;

                if ( groundHit.transform.gameObject.CompareTag( specialAreaTag ) )
                {
                    isTeleportedToSpecialArea = true;
                }
            }
            else
            {
                teleportLocation = player.transform.position;
            }

            teleportAimer.transform.position = teleportLocation + Vector3.up * Config.Global.AIMER_Y_POS;
        }
    }

    public void OnTeleportConfirmationReceived()
    {
        if( isDashing ) return;

        laser.gameObject.SetActive( false );
        teleportAimer.SetActive( false );
        dashStartPosition = player.transform.position;
        isDashing = true;

        if ( isTeleportedToSpecialArea )
        {
            teleportedToSpecialArea.Notify();
        }
    }
#endregion
}

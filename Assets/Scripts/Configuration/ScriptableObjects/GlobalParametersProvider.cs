using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute]
public class GlobalParametersProvider : ScriptableObject
{
    public int CurrentLevel { get; set; }

    public float MAX_TELEPORT_DISTANCE = 15f;

    // Max distance allowed for the user to cast a ray above the ground in order to be a teleport target
    public float MAX_TELEPORT_RAY_HEIGHT = 17f;

    public float DASH_SPEED = 5f;
    public float AIMER_Y_POS = 1f;

    public string[] LEVELS = new string[] { "Level1", "Level2", "Level3", "Level4" };
}

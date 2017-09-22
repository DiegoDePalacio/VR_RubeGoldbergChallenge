using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentConfiguration : MonoBehaviour
{
    [SerializeField] private GlobalParametersProvider globalParameters = null;

    private void Awake()
    {
        Config.Global = globalParameters;
    }
}

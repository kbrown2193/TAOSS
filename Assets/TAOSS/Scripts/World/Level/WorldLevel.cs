using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class to attach to inscene objects, to allow to be found if inactive...
/// 
/// The Worlda Level Data is defined in WorldLevelDatabase
/// </summary>
public class WorldLevel : MonoBehaviour
{
    [SerializeField]
    private string worldLevelKey;

    public string WorldLevelKey
    {
        get { return worldLevelKey; }
    }
}

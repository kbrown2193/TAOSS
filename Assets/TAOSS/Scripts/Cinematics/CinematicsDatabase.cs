using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "CinematicsDatabase", menuName = "Cinematics/Cinematics Database")]
public class CinematicsDatabase : ScriptableObject
{
    [System.Serializable]
    public class CinematicData
    {
        public string cinematicID;
        public PlayableAsset timelineAsset;
    }

    [SerializeField]
    private List<CinematicData> cinematicDataList = new List<CinematicData>();

    private Dictionary<string, PlayableAsset> cinematicLookupTable = new Dictionary<string, PlayableAsset>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        cinematicLookupTable.Clear();
        foreach (CinematicData cinematicData in cinematicDataList)
        {
            cinematicLookupTable[cinematicData.cinematicID] = cinematicData.timelineAsset;
        }
    }

    // Function to retrieve a PlayableAsset based on cinematic ID
    public PlayableAsset GetCinematic(string cinematicID)
    {
        if (cinematicLookupTable.ContainsKey(cinematicID))
        {
            return cinematicLookupTable[cinematicID];
        }
        else
        {
            Debug.LogWarning("Cinematic with ID " + cinematicID + " not found in the database.");
            return null;
        }
    }
}

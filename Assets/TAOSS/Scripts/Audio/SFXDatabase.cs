using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXDatabase", menuName = "Audio/SFX Database")]
public class SFXDatabase : ScriptableObject
{
    [SerializeField] private List<SFXData> sfxDataList = new List<SFXData>();

    private Dictionary<string, SFXData> sfxDataLookupTable = new Dictionary<string, SFXData>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        sfxDataList.Clear();
        foreach (SFXData sfxData in sfxDataList)
        {
            sfxDataLookupTable[sfxData.sfxKey] = sfxData;
        }
    }

    // Function to retrieve a SFX based on sfxKey
    public SFXData GetSFXData(string sfxKey)
    {
        if (sfxDataLookupTable.ContainsKey(sfxKey))
        {
            Debug.Log("SFX.DB.Looking up sfx key = " + sfxKey);
            return sfxDataLookupTable[sfxKey];
        }
        else
        {
            Debug.LogWarning("sfxKey with key " + sfxKey + " not found in the database.");
            return null;
        }
    }

    public AudioClip GetAudioClipFromSFXKey(string sfxKey)
    {
        return GetSFXData(sfxKey).audioClip;
    }
}

[System.Serializable]
public class SFXData
{
    public string sfxKey;

    public AudioClip audioClip; // the file in unity

    // SFX Info
    public string title;
    public string description;
}

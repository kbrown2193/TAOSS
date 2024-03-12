using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDatabase", menuName = "Audio/Music Database")]
public class MusicDatabase : ScriptableObject
{
    [SerializeField] private List<MusicData> musicDataList = new List<MusicData>();

    private Dictionary<string, MusicData> musicDataLookupTable = new Dictionary<string, MusicData>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        musicDataLookupTable.Clear();
        foreach (MusicData musicData in musicDataList)
        {
            musicDataLookupTable[musicData.songKey] = musicData;
        }
    }

    // Function to retrieve a MusicData based on songKey
    public MusicData GetMusicData(string songKey)
    {
        if (musicDataLookupTable.ContainsKey(songKey))
        {
            Debug.Log("Music.DB.Looking up song key = " + songKey);
            return musicDataLookupTable[songKey];
        }
        else
        {
            Debug.LogWarning("songKey with key " + songKey + " not found in the database.");
            return null;
        }
    }

    public AudioClip GetAudioClipFromSongKey(string songKey)
    {
        return GetMusicData(songKey).audioClip;
    }
    public AudioClip GetAudioClipFromWorldLevelKey(string worldLevelKey)
    {
        AudioClip audioClip = null;

        Debug.Log("TODO: find song via world level key"); // to get the world level associated music?, 
        // maybe it will require using the world level database, and that shoudl have a associated song list
        // find...

        return audioClip;
    }
}

[System.Serializable]
public class MusicData
{
    public string songKey; // NOT the "Music" key like key of Eb... its the ID / lookup table key

    public AudioClip audioClip; // the file in unity

    // Song Info
    public string title;
    public string artist;
    public string album; // TODO: make it a proper database and have albums, / album keys referenced and that can lookup
    public string description;
    //public float volume; // default volume this file should be played? may need audio correction if file is too loud? for now just fix by using good files...

    //  World Info (maybe put in WorldLevel(Associated Songs) ... somehow enter level, get a song... ideally to be better optimized
    // for now we will somehow just call song key
    public string[] associatedWorldLevelKeys;
}

[System.Serializable]
public class AlbumData
{
    public string albumKey;

    public string albumTitle;
    public string artist;
    public string[] songs; // make this keys?
    public Sprite albumCoverSprite;
}

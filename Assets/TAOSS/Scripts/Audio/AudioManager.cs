using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSettings audioSettings; // save to user  profile, load from user profile

    const int VOLUME_MAX_INT = 255;

    [SerializeField] private MusicDatabase musicDatabase;
    [SerializeField] private SFXDatabase sfxDatabase;

    [SerializeField] private AudioSource[] musicAudioSources; // todo, handle the switching/ fading better, for now just using 0th index
    [SerializeField] private AudioSource[] sfxAudioSources; // todo, handle the switching/ fading better, for now just using 0th index
    // if here... have atleast 1 sfx audio source here... although can be spawned on other objects? need to do...  update those sources
    // and when they are spawned they should check the sfx volume to set their volumes accordingly

    private int currentMusicChannel = 0; // music channels, for now really testing only 0th index...    
    private int currentSFXChannel = 0;// sfx channels, assume multi?, but for now just one....

    #region Singleton
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("AudioManagerSingleton");
                    instance = singleton.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    private void Start()
    {
        SetAudioSettingsFromApplicationData();
    }

    #region Audio Settings
    public void SetAudioSettingsFromApplicationData()
    {
        audioSettings = ApplicationDataManager.Instance.LoadApplicationData(ApplicationDataManager.Instance.GetUserProfile()).audioSettings;
    }
    
    public void SetAudioSettings(AudioSettings newAudioSettings)
    {
        audioSettings = newAudioSettings;
    }

    public static float CalculateNormalized0To255(int inputValue)
    {
        return inputValue / VOLUME_MAX_INT;
    }

    /// <summary>
    /// Gets the interger value of the overall volume audio setting
    /// </summary>
    /// <returns>int from 0 to 255</returns>
    public int GetVolumeOverall()
    {
        return audioSettings.volumeOverall;
    }
    public int GetVolumeMusic()
    {
        return audioSettings.volumeMusic;
    }
    public int GetVolumeSFX()
    {
        return audioSettings.volumeSFX;
    }

    public float GetNormalizedVolumeOverall()
    {
        return CalculateNormalized0To255(audioSettings.volumeOverall);
    }
    public float GetNormalizedVolumeMusic()
    {
        return CalculateNormalized0To255(audioSettings.volumeMusic);
    }
    public float GetNormalizedVolumeSFX()
    {
        return CalculateNormalized0To255(audioSettings.volumeSFX);
    }
    #endregion

    #region Music Functions
    public void PlayMusic(AudioClip audioClip)
    {
        if (audioClip == null)
        { 
            // fade out of
            // to do... fade out of old and fade into new...
            // for now just play 0th index
            musicAudioSources[currentMusicChannel].clip = audioClip;
        }
    }
    public void PlayMusic(string songKey)
    {
        PlayMusic(GetMusicAudioClip(songKey));
    }

    public AudioClip GetMusicAudioClip(string songKey)
    {
        return musicDatabase.GetAudioClipFromSongKey(songKey);
    }
    public MusicData GetMusicData(string songKey)
    {
        return musicDatabase.GetMusicData(songKey);
    }
    public string GetMusicTitle(string songKey)
    {
        return GetMusicData(songKey).title; // is this the same as...
        //return musicDatabase.GetMusicData(songKey).title;
    }
    public string GetMusicDescription(string songKey)
    {
        return GetMusicData(songKey).description;
    }
    public string GetMusicArtist(string songKey)
    {
        return GetMusicData(songKey).artist;
    }
    public string GetMusicAlbum(string songKey)
    {
        return GetMusicData(songKey).album;
    }
    public string[] GetMusicAssociatedWorldLevels(string songKey)
    {
        return GetMusicData(songKey).associatedWorldLevelKeys;
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSources[currentMusicChannel].volume = volume;
    }
    public void SetMusicVolumeFromAudioSettings()
    {
        SetMusicVolume(audioSettings.volumeMusic);
    }
    public void SetMusicVolumeFromAudioSettings(AudioSettings newAudioSettings)
    {
        SetMusicVolume(newAudioSettings.volumeMusic);
    }
    #endregion

    #region SFX Functions
    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            sfxAudioSources[currentSFXChannel].clip = audioClip;
        }
    }
    public void PlaySFX(string sfxKey)
    {
        PlaySFX(GetSFXAudioClip(sfxKey));
    }

    public AudioClip GetSFXAudioClip(string sfxKey)
    {
        return sfxDatabase.GetAudioClipFromSFXKey(sfxKey);
    }
    public SFXData GetSFXData(string sfxKey)
    {
        return sfxDatabase.GetSFXData(sfxKey);
    }
    public string GetSFXTitle(string sfxKey)
    {
        return GetSFXData(sfxKey).title; // is this the same as
        //return sfxDatabase.GetSFXData(sfxKey).title;
    }
    public string GetSFXDescription(string sfxKey)
    {
        return GetSFXData(sfxKey).description;
    }


    public void SetSFXVolume(float volume)
    {
        sfxAudioSources[currentSFXChannel].volume = volume;
    }
    public void SetSFXVolumeFromAudioSettings()
    {
        SetSFXVolume(audioSettings.volumeSFX);
    }
    public void SetSFXVolumeFromAudioSettings(AudioSettings newAudioSettings)
    {
        SetSFXVolume(newAudioSettings.volumeSFX);
    }
    #endregion
}

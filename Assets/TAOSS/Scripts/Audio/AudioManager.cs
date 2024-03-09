using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSettings audioSettings; // save to user  profile, load from user profile

    const int VOLUME_MAX_INT = 255;

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
}

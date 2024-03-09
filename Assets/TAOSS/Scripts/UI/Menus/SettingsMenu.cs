using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private VideoSettings videoSettings;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private ControlSettings controlSettings;
    [SerializeField] private GameSettings gameSettings;

    [System.Serializable]
    public enum SettingsMenuPage
    {
        Video,
        Audio,
        Controls,
        Game,
        SettingsMenuFadeOut,
    }

    private void Start()
    {
        // Initialize the menu to the Video page
        SetSettingsMenuPage(SettingsMenuPage.Video);
    }

    public void SetSettingsMenuPage(SettingsMenuPage page)
    {
        animator.SetBool("IsVideoMenu", page == SettingsMenuPage.Video);
        animator.SetBool("IsAudioMenu", page == SettingsMenuPage.Audio);
        animator.SetBool("IsControlsMenu", page == SettingsMenuPage.Controls);
        animator.SetBool("IsGameMenu", page == SettingsMenuPage.Game);
        animator.SetBool("IsSettingsMenuFadingOut", page == SettingsMenuPage.SettingsMenuFadeOut);
    }

    // Overloaded function to set the menu page using an integer
    public void SetSettingsMenuPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < (int)SettingsMenuPage.SettingsMenuFadeOut + 1)
        {
            SetSettingsMenuPage((SettingsMenuPage)pageIndex);
        }
        else
        {
            Debug.LogError("Invalid SettingsMenuPage index: " + pageIndex);
        }
    }

    public void LoadSettingsFromProfile()
    {
        ApplicationData applicationData = ApplicationDataManager.Instance.LoadApplicationData(ApplicationDataManager.Instance.GetUserProfile());
        if (applicationData != null)
        {
            videoSettings = applicationData.videoSettings;
            audioSettings = applicationData.audioSettings;
            controlSettings = applicationData.controlSettings;
            gameSettings = applicationData.gameSettings;
        }
        else
        {
            Debug.LogError("UserProfile null");
        }
        Debug.Log("Finished Loading Settings from profile");
    }

    public void SaveSettingsProfile()
    {
        ApplicationDataManager.Instance.SaveSettingsProfiles(videoSettings, audioSettings, controlSettings, gameSettings);
    }

    #region UI Volume Setting
    public void SetVolumeOverall(int value)
    {
        audioSettings.volumeOverall = value;
    }
    public void SetVolumeOverall(float value)
    {
        audioSettings.volumeOverall = Mathf.FloorToInt(value);
    }
    public void SetVolumeMusic(int value)
    {
        audioSettings.volumeMusic = value;
    }
    public void SetVolumeMusic(float value)
    {
        audioSettings.volumeMusic = Mathf.FloorToInt(value); ;
    }
    public void SetVolumeSFX(int value)
    {
        audioSettings.volumeSFX = value;
    }
    public void SetVolumeSFX(float value)
    {
        audioSettings.volumeSFX = Mathf.FloorToInt(value); ;
    }
    public void SetGameSettingTestSetting(int newTestSetting)
    {
        gameSettings.otherSetting = newTestSetting;
    }
    public void SetGameSettingTestSetting(float newTestSetting)
    {
        gameSettings.otherSetting = Mathf.RoundToInt(newTestSetting);
    }
    #endregion

    #region Refreshers
    public void RefreshAudioManager()
    {
        AudioManager.Instance.SetAudioSettings(audioSettings);
    }
    #endregion
}

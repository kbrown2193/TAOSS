using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [System.Serializable] // This attribute is used to make the enum serializable
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
}

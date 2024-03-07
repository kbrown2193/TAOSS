using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public enum MainMenuPage
    {
        TitleMenu, // the "Main Menu" with selections of New Game, Load Game, Continue Game, Settings, an
        SettingsMenu,
        NewGameMenu,
        LoadGameMenu,
        ContinueGameMenu,
        MainMenuFadeOut
    }

    private void Start()
    {
        // Initialize the menu to the TitleMenu page
        SetMainMenuPage(MainMenuPage.TitleMenu);
    }

    public void SetMainMenuPage(MainMenuPage page)
    {
        animator.SetBool("IsTitleMenu", page == MainMenuPage.TitleMenu);
        animator.SetBool("IsSettingsMenu", page == MainMenuPage.SettingsMenu);
        animator.SetBool("IsNewGameMenu", page == MainMenuPage.NewGameMenu);
        animator.SetBool("IsLoadGameMenu", page == MainMenuPage.LoadGameMenu);
        animator.SetBool("IsContinueGameMenu", page == MainMenuPage.ContinueGameMenu);
        animator.SetBool("IsMainMenuFadingOut", page == MainMenuPage.MainMenuFadeOut);
    }

    // Overloaded function to set the menu page using an integer
    public void SetMainMenuPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < (int)MainMenuPage.MainMenuFadeOut)
        {
            SetMainMenuPage((MainMenuPage)pageIndex);
        }
        else
        {
            Debug.LogError("Invalid MainMenuPage index: " + pageIndex);
        }
    }

    // Implementing UIElement interface methods

    public void Show()
    {
        gameObject.SetActive(true);
        Debug.LogWarning("TODO: add additional logic here to initialize the menu as needed");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Debug.LogWarning("TODO: add additional logic here to initialize the menu as needed");
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        Debug.LogWarning("TODO: add additional logic here to toggle visibility as needed");
    }

    public bool IsVisible()
    {
        return gameObject.activeSelf;
    }
}

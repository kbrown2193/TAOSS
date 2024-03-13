using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image backgroundImage;

    // FOR NOW LATER CAN BE MORE EFFICIENT?
    [SerializeField] private Sprite[] backgroundSprites;

    [SerializeField] private Button titleScreenContinueButton; // titleScreen 

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
        if (pageIndex >= 0 && pageIndex <= (int)MainMenuPage.MainMenuFadeOut)
        {
            SetMainMenuPage((MainMenuPage)pageIndex);
        }
        else
        {
            Debug.LogError("Invalid MainMenuPage index: " + pageIndex);
        }
    }

    // Implementing UIElement Visibility
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

    public void TitleMenuInitialization(string gameName)
    {
        Debug.Log("Initialize title screen based off of the game save");
        if (gameName == "VERY_FIRST_GAME_01_23_45_67_89")
        {
            // first startup....
            Debug.Log("Its the very first game....");
            SetTitleMenuBackground(0);

            titleScreenContinueButton.image.color = Color.red;
        }
        else
        {
            // loead checkpoint
            int lastCheckpoint = 0;
            Debug.Log("TODO: load checkopoint and get return a index for background image");

            if (lastCheckpoint <= 7)
            {
                // until some checkpoint set to the default non first startup background...
                SetTitleMenuBackground(1);
            }
            else if (lastCheckpoint <= 14)
            {
                // until some checkpoint set to another background... and so forth
                SetTitleMenuBackground(2);
            }

            ApplicationData applicationData = ApplicationDataManager.Instance.LoadApplicationData();
            if (applicationData.lastPlayed != null)
            {
                if(GameFileHandler.Instance.SaveExists(applicationData.lastPlayed))
                {
                    Debug.Log("Save file exists, continue button is good");
                    titleScreenContinueButton.image.color = Color.green;
                }
            }
        }

        SetMainMenuPage(0);
    }

    public void SetTitleMenuBackground(int titleMenuBackgroundNumber)
    {
        if(titleMenuBackgroundNumber >= 0 && titleMenuBackgroundNumber < backgroundSprites.Length)
        {
            Debug.Log("Valid background index");
            backgroundImage.sprite = backgroundSprites[titleMenuBackgroundNumber];
        }
        else
        {
            Debug.LogError("Invalid background index");
        }
    }

    public void TitileScreenContinuePress()
    {
        GameManager.Instance.TitleScreenContinue(); // continue game
    }
}

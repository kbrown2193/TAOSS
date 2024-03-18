using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is run first on application startup.
/// It is used to do intial loading and setup.
/// </summary>
public class TAOSSPreloader : MonoBehaviour
{
    ApplicationData applicationData;

    [SerializeField] MainMenu mainMenu;

    // wait for awake initializations...
    void Start()
    {
        if(CheckIfApplicationDataExists())
        {
            Debug.Log("Application Data Exists");
            // exists... load user profile settings

            Debug.Log("Loading Application  Data");
            applicationData = ApplicationDataManager.Instance.LoadApplicationData("DEFAULT"); // ONLY LOADING FROM DEFAULT PROFILE FOR NOW
        }
        else
        {
            Debug.Log("Application Data Does NOT Exist");
            // does not exist... create

            Debug.Log("Creating default application data...");
            ApplicationDataManager.Instance.CreateDefaultProfile();

            applicationData = ApplicationDataManager.Instance.LoadApplicationData("DEFAULT");
            Debug.Log("Application Data after: " + applicationData.userProfile + ", " + applicationData.lastPlayed);

            ///applicationData = new ApplicationData("DEFAULT", "VERY_FIRST_GAME_01_23_45_67_89");
            //ApplicationDataManager.Instance.SaveApplicationData("DEFAULT", applicationData);
            // use default...
            // load Main Menu
        }

        InitializeMainMenu(applicationData.lastPlayed);

        //TransitionToMainMenu();
        StartCoroutine(TransitionToMainMenu());
    }

    public bool CheckIfApplicationDataExists()
    {
        bool thereIsApplicationData = false;

        if (ApplicationDataManager.Instance.CheckIfDefaultUserProfileExists())
        {
            thereIsApplicationData = true; // default exists

            Debug.Log("Default profile found");
        }
        else
        {
            // default does not exist yet???
            // or is a user profile

            Debug.Log("No default profile found");

            // check if any other user profiles?
        }

        return thereIsApplicationData;
    }

    public void InitializeMainMenu(string gameName)
    {
        // Ensure MainMenu Existts
        Debug.Log("Initializing Main Menu");

        if(mainMenu == null)
        {
            // null attempt to find?
            // TODO:???

            // error out
            Debug.LogError("No Main Menu Exists!");
            return;
        }

        if (gameName == "VERY_FIRST_GAME_01_23_45_67_89")
        {
            // default setup..
            Debug.Log("Very First Game...");
        }
        else
        {
            // load backgrounds and continue button and settings
        }

        mainMenu.TitleMenuInitialization(gameName);
    }

    public IEnumerator TransitionToMainMenu()
    {
        GameManager.Instance.SetGameState(GameState.MainMenu);
        GameManager.Instance.RefreshPlayerControllerFromGameState();
        //GameManager.Instance.player.SetPlayerControllerControlMode(PlayerControlMode.MainMenu); // achieves same thing? as above

        yield return new WaitForSeconds(2); // initial wait for now... before destroying this

        Destroy(this.gameObject); // delete the preloader for now?

        yield return null;
    }
}

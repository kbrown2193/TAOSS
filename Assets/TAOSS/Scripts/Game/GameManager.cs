using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameData gameData; // this is the currently loaded game data

    [SerializeField] public Player player; // handle the player

    //[SerializeField] public CustomLevelLoadingTAOSS customLevelLoading; // implement way to easily get world level info...

    #region Singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("GameManagerSingleton");
                    instance = singleton.AddComponent<GameManager>();
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

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Application Data Managment

    /// <summary>
    /// DOES NOT SAVE DATA HERE, only sets
    /// use SetApplicationDataLastPlayedAndSave instead if needed
    /// </summary>
    public void SetApplicationDataLastPlayed()
    {
        Debug.LogWarning("Does not save application data here");// 
        string lastPlayed = gameData.gameName;
        ApplicationDataManager.Instance.SetLastPlayed(lastPlayed);
    }
    /// <summary>
    /// DOES NOT SAVE DATA HERE, only sets
    /// use SetApplicationDataLastPlayedAndSave instead if needed
    /// </summary>
    /// <param name="lastPlayed"></param>
    public void SetApplicationDataLastPlayed(string lastPlayed)
    {
        Debug.LogWarning("Does not save application data here");// 
        ApplicationDataManager.Instance.SetLastPlayed(lastPlayed);
    }
    public void SetApplicationDataLastPlayedAndSave()
    {
        string lastPlayed = gameData.gameName;
        ApplicationDataManager.Instance.SetLastPlayedAndSave(lastPlayed);
    }
    public void SetApplicationDataLastPlayedAndSave(string lastPlayed)
    {
        ApplicationDataManager.Instance.SetLastPlayedAndSave(lastPlayed);
    }


    #endregion

    #region Game Data Management
    public void SetReferenceGameData(GameData value)
    {
        this.gameData = value;
    }
    public void SaveGameData()
    {
        GameFileHandler.Instance.Save(gameData.gameName, gameData);
        Debug.Log("Game Saved time at " + Time.time);
        Debug.Log("Game Saved Location " + GameFileHandler.Instance.GetGameSaveFilePath(gameData.gameName));
    }
    public void LoadSave(string gameName)
    {
        //LoadGameData(gameName);
        Debug.LogWarning("Unused???"); // using loadgamedata ?
    }
    public void LoadGameData(string gameName)
    {
        Debug.LogWarning("Game Loaded Location " + GameFileHandler.Instance.GetGameSaveFilePath(gameData.gameName));
        Debug.LogWarning("Game Load time Begin at: " + Time.time);
        GameData tmpGameData = GameFileHandler.Instance.LoadSave(gameName);
        if (tmpGameData != null)
        {
            gameData = tmpGameData;
        }
        else
        {
            Debug.LogError("tmp Game Data is Null");
        }
        Debug.LogWarning("Game Load time End at " + Time.time);
    }
    public GameData GetGameData()
    {
        return gameData;
    }

    public void SaveNewCheckpoint(int checkpoint)
    {
        Debug.Log("Saving new checkpoint, was " + gameData.lastCheckpoint);
        gameData.lastCheckpoint = checkpoint;
        Debug.Log("Saving new checkpoint, is now " + gameData.lastCheckpoint);
        SaveGameData();
    }

    public void SaveNewPlayerCharacterVisualData(PlayerCharacterVisualData value)
    {
        Debug.Log("Saving new playerCharacterVisualData, name was" + gameData.playerCharacterVisualData.characaterName);
        Debug.Log("Saving new playerCharacterVisualData, headSelection was" + gameData.playerCharacterVisualData.headSelection);
        gameData.playerCharacterVisualData = value;
        Debug.Log("Saving new playerCharacterVisualData, headSelection is now" + gameData.playerCharacterVisualData.headSelection);
        SaveGameData();
    }
    public void SaveLifetimeCampaignData(LifetimeCampaignData newLifetimeCampaignData)
    {
        Debug.Log("TODO: Saving Campaign Data... ");
        gameData.lifetimeCampaignData = newLifetimeCampaignData;
        SaveGameData();
    }

    public void SaveCampaignData(CampaignData campaignData)
    {
        Debug.Log("TODO: Saving Campaign Data... ");
        gameData.campaignData = campaignData;
        SaveGameData();
    }
    public void SaveGatewayLockDatas(LockData[] lockDatas)
    {
        Debug.Log("TESTING: Saving Gateway Lock Data... ");
        gameData.gatewaylockDatas = lockDatas;
        SaveGameData();
    }


    public int GetCurrentCheckpoint()
    {
        return gameData.lastCheckpoint;
    }
    #endregion

    #region Game Init
    public void TitleScreenContinue()
    {

        Debug.Log("GM.TitleScreenContinue");
        // ensure last played exists
        ApplicationData applicationData = ApplicationDataManager.Instance.LoadApplicationData();
        if (applicationData.lastPlayed != null)
        {
            if (GameFileHandler.Instance.SaveExists(applicationData.lastPlayed))
            {
                Debug.Log("Save file exists");
                LoadGameInit(applicationData.lastPlayed);
            }
        }
        //if(gameData != )
    }
    public void LoadGameInit(string gameName)
    {
        // Load the selected game save
        Debug.Log("Loading game save: " + gameName);
        GameData gameData = GameFileHandler.Instance.LoadSave(gameName);

        Debug.Log("TODO: load to currrent checkpoint");
        if (gameData != null)
        {
            GameManager.Instance.SetReferenceGameData(gameData);

            // game data exists, so checpoint check...
            if (gameData.lastCheckpoint == 0)
            {
                // default initial start
                Debug.Log("Last Checkpoint = 0, playing intro cinematic");
                CinematicsManager.Instance.PlayCinematic("TAOSS_C0_L00_00_Intro");
                UIManager.Instance.GetMainMenu().SetMainMenuPage(MainMenu.MainMenuPage.MainMenuFadeOut); // set main menu fade out page
            }
            else
            {
                Debug.Log("TODO Implement where to load for this checkpoint" + gameData.lastCheckpoint.ToString());
                CinematicsManager.Instance.PlayCinematic("TAOSS_C0_L00_00_Intro"); // CHANGE THIS?
                UIManager.Instance.GetMainMenu().SetMainMenuPage(MainMenu.MainMenuPage.MainMenuFadeOut); // set main menu fade out page
                GameManager.Instance.LoadGameData(gameData.gameName);
            }
        }
        else
        {
            Debug.LogWarning("No Game Data Exists!");
        }
    }
    #endregion

    #region Player Management
    public void EnablePlayerMovment()
    {
        //player.is
        Debug.LogWarning("Enabling player movment");
        player.SetIsMovemenEnabled(true);
        //player.RefreshPlayerControllers(); //this handles enabling whichever mode is in?
    }
    public void DisablePlayerMovment()
    {
        //player.is
        Debug.LogWarning("Disablng player movment");
        player.SetIsMovemenEnabled(false);
        //player.RefreshPlayerControllers(); //this handles enabling whichever mode is in?
    }
    public void SetPlayerMovment(bool value)
    {
        //player.is
        Debug.LogWarning("Setting player movement to " + value );
        player.SetIsMovemenEnabled(value);
        //player.RefreshPlayerControllers(); //this handles enabling whichever mode is in?
    }
    public void SetPlayerMovementMode(PlayerMovementMode playerMovementMode)
    {
        player.SetPlayerMovementMode(playerMovementMode);
    }
    public void SetPlayerMovementMode(int playerMovementMode)
    {
        player.SetPlayerMovementMode((PlayerMovementMode) playerMovementMode);
    }

    public void ReparentPlayer(Transform value)
    {
        player.ReparentPlayer(value);
    }
    public void RepositionPlayer(Vector3 value)
    {
        player.RepositionPlayer(value);
    }
    public void SetPlayerWorldLevelSizeScaler(float value)
    { 
        player.SetWorldLevelSizeScaler(value);
    }
    public void SetPlayerWorldLevelSpeedMultiplier(float value)
    {
        player.SetPlayerWorldLevelSpeedMultiplier(value);
    }

    public void SetPlayer(Player newPlayer)
    {
        if (newPlayer != null)
        {
            if (newPlayer == player)
            {
                Debug.LogWarning("Warning Attempting to set same player object ");
            }
            else
            {
                Debug.Log("Setting new player from " + player.name + " to " + newPlayer.name);
                player = newPlayer;
            }
        }
        else
        {
            Debug.LogWarning("Player = null");
        }
    }
    public Player GetPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player is Null");
            return null;
        }
        else
        {
            return player;
        }
    }
    #endregion
}

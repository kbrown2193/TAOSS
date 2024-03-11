using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameData gameData; // this is the currently loaded game data

    [SerializeField] public Player player; // handle the player

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
    public void LoadGameData(string gameName)
    {
        gameData = GameFileHandler.Instance.LoadSave(gameName);
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

    public int GetCurrentCheckpoint()
    {
        return gameData.lastCheckpoint;
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

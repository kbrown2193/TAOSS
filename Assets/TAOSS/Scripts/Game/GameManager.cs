using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameData gameData; // this is the currently loaded game data

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ApplicationDataManager : MonoBehaviour
{
    private string userProfile = "DEFAULT";
    private ApplicationData applicationData;

    private string saveFolderName = "UserProfiles";
    private string saveFolderPath;

    //private string lastPlayedGame = ""; should be in application settings....

    // Constant value for the file extension
    private const string fileExtension = ".json";

    #region Singleton
    private static ApplicationDataManager instance;

    public static ApplicationDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ApplicationDataManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("ApplicationDataManagerSingleton");
                    instance = singleton.AddComponent<ApplicationDataManager>();
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

        saveFolderPath = Path.Combine(Application.persistentDataPath, saveFolderName);

        // Create the user profile save folder if it doesn't exist
        if (!Directory.Exists(saveFolderPath))
        {
            Debug.Log("Creating User Profiles Directory");
            Directory.CreateDirectory(saveFolderPath);

            // There probably isnt a default profile but lets check to be sure
            if (CheckIfDefaultUserProfileExists())
            {
                // there is default profile somehow
                Debug.Log("There is a default profile file, no action needed to create file");
            }
            else
            {
                // Create for first time setup
                CreateDefaultProfile();
            }
        }
    }
    #endregion

    public string GetUserProfile()
    {
        // maybe ensure it is up to date?

        return userProfile;
    }

    /// <summary>
    /// Save file name should be the userProfile
    /// </summary>
    /// <param name="saveFileName"></param>
    /// <param name="newApplicationData"></param>
    public void SaveApplicationData(string saveFileName, ApplicationData newApplicationData)
    {
        userProfile = saveFileName;
        //string userProfile, string lastPlayed
        applicationData = newApplicationData;

        string fullSaveFileName = saveFileName + fileExtension;
        string saveFilePath = Path.Combine(saveFolderPath, fullSaveFileName);

        // Serialize the updated game data to JSON
        string json = JsonUtility.ToJson(applicationData);

        // Overwrite the existing save file with the updated data
        File.WriteAllText(saveFilePath, json);
    }
    public void SaveCurrentApplicationData(string saveFileName)
    {
        userProfile = saveFileName;
        //string userProfile, string lastPlayed
        //applicationData = newApplicationData;

        string fullSaveFileName = saveFileName + fileExtension;
        string saveFilePath = Path.Combine(saveFolderPath, fullSaveFileName);

        // Serialize the updated game data to JSON
        string json = JsonUtility.ToJson(applicationData);

        // Overwrite the existing save file with the updated data
        File.WriteAllText(saveFilePath, json);
    }

    // Load a application data
    public ApplicationData LoadApplicationData(string saveFileName)
    {
        string fullSaveFileName = saveFileName + fileExtension;
        string saveFilePath = Path.Combine(saveFolderPath, fullSaveFileName);

        // Check if the save file exists before attempting to load it
        if (File.Exists(saveFilePath))
        {
            Debug.Log("Loading application data at " + saveFilePath);
            // Read the JSON data from the file
            string json = File.ReadAllText(saveFilePath);

            // Deserialize the JSON data to GameData object
            ApplicationData newApplicationData = JsonUtility.FromJson<ApplicationData>(json);
            applicationData = newApplicationData;
            return applicationData;
        }
        else
        {
            Debug.LogError("Save file does not exist: " + fullSaveFileName);
            return null;
        }
    }
    // Load a application data
    /// <summary>
    /// if no arguments, use userprofiel
    /// </summary>
    /// <returns></returns>
    public ApplicationData LoadApplicationData()
    {
        string fullSaveFileName = userProfile + fileExtension;
        string saveFilePath = Path.Combine(saveFolderPath, fullSaveFileName);

        // Check if the save file exists before attempting to load it
        if (File.Exists(saveFilePath))
        {
            Debug.Log("Loading application data at " + saveFilePath);
            // Read the JSON data from the file
            string json = File.ReadAllText(saveFilePath);

            // Deserialize the JSON data to GameData object
            ApplicationData newApplicationData = JsonUtility.FromJson<ApplicationData>(json);
            applicationData = newApplicationData;
            return applicationData;
        }
        else
        {
            Debug.LogError("Save file does not exist: " + fullSaveFileName);
            return null;
        }
    }

    public void CreateDefaultProfile()
    {
        Debug.Log("Creating Default User Profile");
        string saveFileName = "DEFAULT";
        ApplicationData applicationData = new ApplicationData("DEFAULT", "VERY_FIRST_GAME_01_23_45_67_89");
        SaveApplicationData(saveFileName, applicationData);
    }
    public bool CheckIfDefaultUserProfileExists()
    {
        bool thereIsDefaultUserProfile = false;

        string saveFileName = "DEFAULT";
        string fullSaveFileName = saveFileName + fileExtension;
        string saveFilePath = Path.Combine(saveFolderPath, fullSaveFileName);
        //string saveFilePath = Path.Combine(saveFolderPath, saveFileName + fileExtension);

        Debug.Log("Attempting to find default profile at path = " + saveFilePath);

        if (SaveExistsAtFilePath(saveFilePath))
        {
            Debug.Log("There is a default user profile");
            thereIsDefaultUserProfile = true;
        }
        else
        {
            Debug.Log("There is NOT a default user profile");
        }

        return thereIsDefaultUserProfile;
    }

    // REUSED FUNCTION, TODO: refactor into a file IO static class to handle these functions... for simplicity right now, keeping here, maybe make it virtual?
    // Check if a specific game save file exists 
    public bool SaveExistsAtFileName(string saveFileName)
    {
        string saveFilePath = Path.Combine(saveFolderPath, saveFileName + fileExtension);
        return File.Exists(saveFilePath);
    }
    public bool SaveExistsAtFilePath(string saveFilePath)
    {
        return File.Exists(saveFilePath);
    }

    #region Settings Saving
    public void SaveNewVideoSettings(VideoSettings newVideoSettings)
    {
        applicationData.videoSettings = newVideoSettings;
        SaveApplicationData(applicationData.userProfile, applicationData);
    }
    public void SaveNewAudioSettings(AudioSettings newAudioSettings)
    {
        applicationData.audioSettings = newAudioSettings;
        SaveApplicationData(applicationData.userProfile, applicationData);
    }
    public void SaveNewControlSettings(ControlSettings newControlSettings)
    {
        applicationData.controlSettings = newControlSettings;
        SaveApplicationData(applicationData.userProfile, applicationData);
    }
    public void SaveNewGameSettings(GameSettings newGameSettings)
    {
        applicationData.gameSettings = newGameSettings;
        SaveApplicationData(applicationData.userProfile, applicationData);
    }
    public void SaveSettingsProfiles(VideoSettings videoSettings, AudioSettings audioSettings, ControlSettings controlSettings, GameSettings gameSettings)
    {
        applicationData.videoSettings = videoSettings;
        applicationData.audioSettings = audioSettings;
        applicationData.controlSettings = controlSettings;
        applicationData.gameSettings = gameSettings;
        SaveApplicationData(applicationData.userProfile, applicationData);
    }
    #endregion

    #region Last Played...
    public void SetLastPlayed(string lastPlayed)
    {
        applicationData.lastPlayed = lastPlayed;
    }
    public void SetLastPlayedAndSave(string lastPlayed)
    {
        applicationData.lastPlayed = lastPlayed;
        SaveCurrentApplicationData(applicationData.userProfile);
    }
    #endregion
}

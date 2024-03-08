using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ApplicationDataManager : MonoBehaviour
{
    private string userProfile;
    private ApplicationData ApplicationData;


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

            if(CheckIfDefaultUserProfileExists())
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


    public void SaveApplicationData(string saveFileName, ApplicationData applicationData)
    {
        //string userProfile, string lastPlayed

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
            // Read the JSON data from the file
            string json = File.ReadAllText(saveFilePath);

            // Deserialize the JSON data to GameData object
            ApplicationData applicationData = JsonUtility.FromJson<ApplicationData>(json);
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


}

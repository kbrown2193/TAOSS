using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //private string saveFolderName = "Dialog"; // or is it Resources?7
   // private string saveFolderPath;

    //private string lastPlayedGame = ""; should be in application settings....

    // Constant value for the file extension
    //private const string fileExtension = ".json";

    #region Singleton
    private static DialogManager instance;

    public static DialogManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("DialogManagerSingleton");
                    instance = singleton.AddComponent<DialogManager>();
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

        //saveFolderPath = Path.Combine(Application.persistentDataPath, saveFolderName);

        // Create the save folder if it doesn't exist
        //if (!Directory.Exists(saveFolderPath))
       // {
        //    Directory.CreateDirectory(saveFolderPath);
        //}
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public string GetDialogLine(string dialogKey)
    {
        string line = "TODO";
        Debug.LogWarning("TODO: Fetch dialog line");
        return line;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogDatabase dialogDatabase;
    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private DialogResponseOption[] dialogResponseOptions;

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

    #region Dialog Data Retrieval
    public DialogData GetDialogData(string dialogKey)
    {
        return dialogDatabase.GetDialogData(dialogKey);
    }
    public string GetDialogLine(string dialogKey)
    {
        return GetDialogData(dialogKey).dialogText;
    }
    public DialogType GetDialogType(string dialogKey)
    {
        return GetDialogData(dialogKey).dialogType;
    }
    public string[] GetDialogResponses(string dialogKey)
    {
        return GetDialogData(dialogKey).dialogResponses;
    }
    #endregion

    #region Dialog Visiblity
    public void ShowDialogBox()
    {
        dialogBox.Show();
    }
    public void HideDialogBox()
    {
        dialogBox.Hide();
    }
    public void ShowDialogResponseOptions()
    {
        for(int i = 0; i < dialogResponseOptions.Length; i++)
        {
            dialogResponseOptions[i].Show();
        }
        /*
        foreach(DialogResponseOption option in dialogResponseOptions)
        {
            option.Show();
        }
        */
    }
    public void ShowDialogResponseOptions(int responseCount)
    {
        for (int i = 0; i < responseCount; i++)
        {
            dialogResponseOptions[i].Show();
        }
        /*
        foreach(DialogResponseOption option in dialogResponseOptions)
        {
            option.Show();
        }
        */
    }
    public void HideDialogResponseOptions()
    {
        for(int i = 0; i < dialogResponseOptions.Length; i++)
        {
            dialogResponseOptions[i].Hide();
        }
    }
    /// <summary>
    /// maybe unecessary, could just always call hide all?
    /// </summary>
    /// <param name="responseCount"></param>
    public void HideDialogResponseOptions(int responseCount)
    {
        for (int i = 0; i <responseCount; i++)
        {
            dialogResponseOptions[i].Hide();
        }
    }

    #endregion

    #region Typing Dialog
    public void TypeDialog(string dialogText)
    {
        dialogBox.TypeText(dialogText);
    }
    public void TypeDialogByDialogKey(string dialogKey)
    {
        dialogBox.TypeText(GetDialogLine(dialogKey));
    }

    public void TypeDialogResponse(int responseIndex, string resoponseText)
    {
        dialogResponseOptions[responseIndex].TypeResponseText(resoponseText);
    }
    public void TypeDialogResponses(string dialogKey)
    { 
        DialogData responseDialogData = GetDialogData (dialogKey);
        int responseCount = responseDialogData.dialogResponses.Length;

        for (int i = 0; i < responseCount; i++)
        { 
            dialogResponseOptions[i].TypeResponseText(responseDialogData.dialogResponses[i]);
        }
    }
    #endregion
}

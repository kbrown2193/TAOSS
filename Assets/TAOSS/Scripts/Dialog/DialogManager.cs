using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogDatabase dialogDatabase;
    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private DialogResponseOption[] dialogResponseOptions;

    private DialogStatus currentDialogStatus; // the currentDialog Stats

    private string currentDialogKey; // hold a key to which dialog we are at... // see currentDialogStatus.dialogKey

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

    #region Dialog Begin and End
    public void BeginDialog(string dialogKey)
    {
        Debug.Log("Begining Dialog");
        ShowDialogBox();
        TypeDialogTextByDialogKey(dialogKey);
        ShowDialogResponseOptions(dialogKey);
        TypeDialogResponses(dialogKey);
    }
    public void EndDialog()
    {
        Debug.Log("Ending Dialog");
        HideDialogBox();
        HideDialogResponseOptions();
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
    /// <summary>
    /// Shows all response options
    /// </summary>
    public void ShowDialogResponseOptions()
    {
        for(int i = 0; i < dialogResponseOptions.Length; i++)
        {
            dialogResponseOptions[i].Show();
        }
    }
    /// <summary>
    /// Shows the amount of responses based on the dialogKey
    /// </summary>
    /// <param name="dialogKey"></param>
    public void ShowDialogResponseOptions(string dialogKey)
    {
        int responseCount = GetDialogData(dialogKey).dialogResponses.Length;

        for (int i = 0; i < responseCount; i++)
        {
            dialogResponseOptions[i].Show();
        }
    }
    /// <summary>
    /// Shows a specified amount of response options
    /// </summary>
    /// <param name="responseCount"></param>
    public void ShowDialogResponseOptions(int responseCount)
    {
        for (int i = 0; i < responseCount; i++)
        {
            dialogResponseOptions[i].Show();
        }
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
    /// <summary>
    /// INCASE WANT TO DIRECTLY SET DIALOG TEXT USE THIS, ELSE USE By dialogKey
    /// </summary>
    /// <param name="dialogText"></param>
    public void TypeDialogText(string dialogText)
    {
        Debug.Log("DM.Typing Dialog" + dialogText);
        dialogBox.TypeText(dialogText);
    }
    public void TypeDialogTextByDialogKey(string dialogKey)
    {
        Debug.Log("DM.Typing Dialog by key" + dialogKey);
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

public class DialogStatus
{
    public DialogOpenState dialogOpenState;
    public DialogType dialogType;
    public string dialogKey;
    public int hoveredResponse;
    public int mostRecentResponse; // what was the last key

}
[System.Serializable]
public enum DialogOpenState
{
    Closed, // there is no dialog at the momment
    Open, // the dialog is open
}

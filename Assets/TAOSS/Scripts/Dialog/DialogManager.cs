using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogDatabase dialogDatabase;
    [SerializeField] private DialogBox dialogBox;
    [SerializeField] private DialogResponseOption[] dialogResponseOptions;

    private DialogStatus dialogStatus; // the currentDialog Stats

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

        // Initialize Dialog Status
        dialogStatus = new DialogStatus();
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
    public int GetDialogResponseCount(string dialogKey)
    {
        return GetDialogData(dialogKey).dialogResponses.Length;
    }
    #endregion

    #region Dialog Begin and End
    public void BeginDialog(string dialogKey)
    {
        Debug.Log("Begining Dialog");
        currentDialogKey = dialogKey;
        DialogData dialogData = GetDialogData(dialogKey);

        ShowDialogBox();
        TypeDialogTextByDialogKey(dialogKey);
        ShowDialogResponseOptions(dialogKey);
        TypeDialogResponses(dialogKey);
        SetDialogStatus(dialogKey);
    }
    public void EndDialog()
    {
        Debug.Log("Ending Dialog");
        HideDialogBox();
        HideDialogResponseOptions();
        dialogStatus.dialogOpenState = DialogOpenState.Closed;
    }
    #endregion

    #region Player Interaction 
    public void PlayerDialogInteract()
    {
        DialogData dialogData = GetDialogData(currentDialogKey); //

        if(dialogStatus.dialogOpenState != DialogOpenState.Closed)
        {
            switch (dialogStatus.dialogType)
            {
                case DialogType.NoResponseAuto:
                    // interactionn does nothing?
                    Debug.Log("PlayerDialogInterac.NoResponseAuto... Do nothing for now...or hide");
                    if (dialogData.isDialogChain)
                    {
                        Debug.Log("Is Dialog Chain");
                        BeginDialog(dialogData.nextDialogKey);
                    }
                    else
                    {
                        Debug.Log("Is NOT Dialog Chain");
                        // is not dialog chain, so end
                        EndDialog();
                    }
                    break;
                case DialogType.NoResponseButConfirmation:
                    // either continue to next dialog... or hide...
                    Debug.Log("PlayerDialogInteract.NoResponseButConfirmation... Hide dialog for now...");
                    if (dialogData.isDialogChain)
                    {
                        Debug.Log("Is Dialog Chain");
                        BeginDialog(dialogData.nextDialogKey);
                    }
                    else
                    {
                        Debug.Log("Is NOT Dialog Chain");
                        // is not dialog chain, so end
                        EndDialog();
                    }
                    break;
                case DialogType.WithResponses:
                    Debug.Log("PlayerDialogInteract.WithResponses... Choose Response...");
                    ChooseResponse(dialogStatus.hoveredResponse);
                    break;
            }
        }
        Debug.Log("The Dialog is closed...");
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

    #region Dialog Status Stuff
    public void SetDialogStatus(DialogStatus newDialogStatus)
    {
        dialogStatus = newDialogStatus;
    }
    public void SetDialogStatus(string dialogKey)
    {
        dialogStatus.dialogKey = dialogKey;
        dialogStatus.dialogOpenState = DialogOpenState.Open;
        dialogStatus.dialogType = GetDialogType(dialogKey);
        dialogStatus.responseCount =  GetDialogResponseCount(dialogKey);
        dialogStatus.hoveredResponse = 0; // reset choice to 0.
    }

    public DialogStatus GetDialogStatus()
    {
        return dialogStatus;
    }
    #endregion

    #region Responses
    public void ChooseResponse(int responseChoice)
    {
        DialogData dialogData = GetDialogData(currentDialogKey);

        Debug.Log("Response Choice " + responseChoice);
        Debug.Log("TODO: Save Choice " + responseChoice);
        dialogStatus.mostRecentResponse = responseChoice;

        if (dialogData != null)
        {
            if (dialogData.isDialogChain)
            {
                Debug.Log("Is Dialog Chain");
                BeginDialog(dialogData.nextDialogKey);
            }
            else
            {
                Debug.Log("Is NOT Dialog Chain");
                // is not dialog chain, so end
                EndDialog();
            }
        }
        else
        {
            Debug.Log("Curent dialog data is null");
        }
    }

    public void SetHoveredResponse(int response)
    {
        dialogStatus.hoveredResponse = response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>success</returns>
    public bool IncreaseHoveredResponse()
    {
        if(dialogStatus.hoveredResponse < dialogStatus.responseCount)
        {
            dialogStatus.hoveredResponse++;
            return true;
        }
        else
        {
            Debug.Log("At max response");
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>success</returns>
    public bool DeceaseHoveredResponse()
    {
        if (dialogStatus.hoveredResponse > 0)
        {
            dialogStatus.hoveredResponse--;
            return true;
        }
        else
        {
            Debug.Log("At min response");
            return false;
        }
    }
    #endregion
}

public class DialogStatus
{
    public string dialogKey; // the current dialog key
    public DialogOpenState dialogOpenState; // is the dialog open or closed
    public DialogType dialogType; // what dialog type do we have
    public int responseCount; // current response count
    public int hoveredResponse; // what option is player 
    public int mostRecentResponse; // what was the last key

}
[System.Serializable]
public enum DialogOpenState
{
    Closed, // there is no dialog at the momment
    Open, // the dialog is open
}

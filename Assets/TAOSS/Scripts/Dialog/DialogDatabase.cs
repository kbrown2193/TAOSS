using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogDatabase", menuName = "Dialog/Dialog Database")]
public class DialogDatabase : ScriptableObject
{
    [SerializeField] private List<DialogData> dialogDataList = new List<DialogData>();

    private Dictionary<string, DialogData> dialogDataLookupTable = new Dictionary<string, DialogData>();

    private void OnEnable()
    {
        // Populate the lookup table when the scriptable object is loaded
        InitializeLookupTable();
    }

    private void InitializeLookupTable()
    {
        dialogDataLookupTable.Clear();
        foreach (DialogData dialogData in dialogDataList)
        {
            dialogDataLookupTable[dialogData.dialogKey] = dialogData;
        }
    }
    // Function to retrieve a MusicData based on songKey
    public DialogData GetDialogData(string dialogKey)
    {
        if (dialogDataLookupTable.ContainsKey(dialogKey))
        {
            Debug.Log("Dialog.DB.Looking up song key = " + dialogKey);
            return dialogDataLookupTable[dialogKey];
        }
        else
        {
            Debug.LogWarning("dialogKey with key " + dialogKey + " not found in the database.");
            return null;
        }
    }
}

[System.Serializable]
public class DialogData
{
    public string dialogKey;
    public DialogType dialogType;
    public string dialogText;
    public string[] dialogResponses;
    public bool isDialogChain;
    public string nextDialogKey; // if is dialog chain... then go ehere next
}


[System.Serializable]
public enum DialogType
{
    NoResponseAuto, //  no resposne, will automatically continue...
    NoResponseButConfirmation, // no response... requires player to input to continue..
    WithResponses,
                    //TwoResponses, // the most common binary, 1 or 0, yes or no option .. could be unecessary.... lets just check if response count is 2?
    //MultipleResponses, // c
}

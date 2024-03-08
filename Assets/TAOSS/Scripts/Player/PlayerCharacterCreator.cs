using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCharacterCreator : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Image[] partImages = new Image[5]; // with index from partNumber

    [SerializeField] private Sprite[] hairSelections;
    [SerializeField] private Sprite[] headSelections;
    [SerializeField] private Sprite[] torsoSelections;
    [SerializeField] private Sprite[] legsSelections;
    [SerializeField] private Sprite[] feetSelections;

    private string characaterName;
    private int hairSelection;
    private int headSelection;
    private int torsoSelection;
    private int legsSelection;
    private int feetSelection;

    // TODO: Colors...
    private Color hairColor;
    private Color headColor;
    private Color torsoColor;
    private Color legsColor;
    private Color feetColor;
    private Color skinColor;
    private Color eyeColor;

    private PlayerCharacterVisualData playerCharacterVisualData;

    public enum CharacterModelPart
    {
        Hair,
        Head,
        Torso,
        Legs,
        Feet
    }

    private void Awake()
    {
        if (playerCharacterVisualData == null)
        {
            playerCharacterVisualData = new PlayerCharacterVisualData();
        }
        else
        {
            Debug.Log("playerCharacterVisualData already not null");
        }
    }

    #region Character Part Index Selection
    public void SetPartIndex(CharacterModelPart characterModelPart, int partIndex)
    {
        switch (characterModelPart)
        {
            case CharacterModelPart.Hair:
                if(partIndex < hairSelections.Length)
                {
                    hairSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Head:
                if (partIndex < headSelections.Length)
                {
                    headSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Torso:
                if (partIndex < torsoSelections.Length)
                {
                    torsoSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Legs:
                if (partIndex < legsSelections.Length)
                {
                    legsSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Feet:
                if (partIndex < feetSelections.Length)
                {
                    feetSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            default:
                break;
        }
    }
    public void SetPartIndex(int characterModelPart, int partIndex)
    {
        switch ((CharacterModelPart)characterModelPart)
        {
            case CharacterModelPart.Hair:
                if (partIndex < hairSelections.Length)
                {
                    hairSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Head:
                if (partIndex < headSelections.Length)
                {
                    headSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Torso:
                if (partIndex < torsoSelections.Length)
                {
                    torsoSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Legs:
                if (partIndex < legsSelections.Length)
                {
                    legsSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            case CharacterModelPart.Feet:
                if (partIndex < feetSelections.Length)
                {
                    feetSelection = partIndex;
                }
                else
                {
                    Debug.LogError("Invalid index");
                }
                break;
            default:
                break;
        }
    }


    public void SetHairIndex(int value)
    {
        SetPartIndex(CharacterModelPart.Hair, value);
    }
    public void SetHeadIndex(int value)
    {
        SetPartIndex(CharacterModelPart.Head, value);
    }
    public void SetTorsoIndex(int value)
    {
        SetPartIndex(CharacterModelPart.Torso, value);
    }
    public void SetLegsIndex(int value)
    {
        SetPartIndex(CharacterModelPart.Legs, value);
    }
    public void SetFeetIndex(int value)
    {
        SetPartIndex(CharacterModelPart.Feet, value);
    }
    public void SetHairIndex(float value)
    {
        SetPartIndex(CharacterModelPart.Hair, Mathf.RoundToInt(value));
    }
    public void SetHeadIndex(float value)
    {
        SetPartIndex(CharacterModelPart.Head, Mathf.RoundToInt(value));
    }
    public void SetTorsoIndex(float value)
    {
        SetPartIndex(CharacterModelPart.Torso, Mathf.RoundToInt(value));
    }
    public void SetLegsIndex(float value)
    {
        SetPartIndex(CharacterModelPart.Legs, Mathf.RoundToInt(value));
    }
    public void SetFeetIndex(float value)
    {
        SetPartIndex(CharacterModelPart.Feet, Mathf.RoundToInt(value));
    }

    public void IncreasePartIndex(CharacterModelPart characterModelPart)
    {
        switch (characterModelPart)
        {
            case CharacterModelPart.Hair:
                if(hairSelection < hairSelections.Length - 1)
                {
                    hairSelection++; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Head:
                if (headSelection < headSelections.Length - 1)
                {
                    headSelection++; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Torso:
                if (torsoSelection < torsoSelections.Length - 1)
                {
                    torsoSelection++; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Legs:
                if (legsSelection < legsSelections.Length - 1)
                {
                    legsSelection++; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Feet:
                if (feetSelection < feetSelections.Length - 1)
                {
                    feetSelection++; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            default:
                break;

        }
    }
    public void IncreasePartIndex(int characterModelPart)
    {
        IncreasePartIndex((CharacterModelPart)characterModelPart);        
    }
    public void DecreasePartIndex(CharacterModelPart characterModelPart)
    {
        switch (characterModelPart)
        {
            case CharacterModelPart.Hair:
                if (hairSelection > 0 )
                {
                    hairSelection--; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Head:
                if (headSelection > 0 )
                {
                    headSelection--; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Torso:
                if (torsoSelection > 0)
                {
                    torsoSelection--; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Legs:
                if (legsSelection > 0)
                {
                    legsSelection--; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            case CharacterModelPart.Feet:
                if (feetSelection > 0)
                {
                    feetSelection--; // valid, increase index
                }
                else
                {
                    // end... could wrap, for now... nothing
                }
                break;
            default:
                break;

        }
    }
    public void DecreasePartIndex(int characterModelPart)
    {
        DecreasePartIndex((CharacterModelPart)characterModelPart);        
    }
    #endregion

    #region Character Name Info
    public void SetCharacterName(string value)
    {
        characaterName = value;
    }
    public void SetCharacterNameViaInputField()
    {
        characaterName = nameInputField.text;
    }

    #endregion

    #region Character Previewing
    public void RefreshCharacterImages()
    {
        partImages[(int)CharacterModelPart.Hair].sprite = hairSelections[hairSelection];
        partImages[(int)CharacterModelPart.Head].sprite = headSelections[headSelection];
        partImages[(int)CharacterModelPart.Torso].sprite = torsoSelections[torsoSelection];
        partImages[(int)CharacterModelPart.Legs].sprite = legsSelections[legsSelection];
        partImages[(int)CharacterModelPart.Feet].sprite = feetSelections[feetSelection];

        // todo: colors...
    }

    #endregion

    #region Character Creation and Export
    public void CreateCharacterButtonPress()
    {
        CreateCharacter(); // Create and Save the newly created character
        Debug.Log("TODO:then start game");
    }
    public void CreateCharacter()
    {
        // Ensure values REFRESH Values
        if (ValidateCharacterName())
        {
            characaterName = nameInputField.text; // IF VALID... todo...
        }
        else
        {
            characaterName = "Bad Name Given";
        }

        playerCharacterVisualData.characaterName = characaterName;

        playerCharacterVisualData.hairSelection = hairSelection;
        playerCharacterVisualData.headSelection = headSelection;
        playerCharacterVisualData.torsoSelection = torsoSelection;
        playerCharacterVisualData.legsSelection = legsSelection;
        playerCharacterVisualData.feetSelection = feetSelection;

        playerCharacterVisualData.hairColor = hairColor;
        playerCharacterVisualData.headColor = headColor;
        playerCharacterVisualData.torsoColor = torsoColor;
        playerCharacterVisualData.legsColor = legsColor;
        playerCharacterVisualData.feetColor = feetColor;
        playerCharacterVisualData.skinColor = skinColor;
        playerCharacterVisualData.eyeColor = eyeColor;

        Debug.Log("Creating character ...");
        GameManager.Instance.SaveNewPlayerCharacterVisualData(playerCharacterVisualData);
    }

    public PlayerCharacterVisualData ExportPlayerCharacterVisualData()
    {
        // ensure everthing is what it currently is...
        playerCharacterVisualData.characaterName = characaterName;

        playerCharacterVisualData.hairSelection = hairSelection;
        playerCharacterVisualData.headSelection = headSelection;
        playerCharacterVisualData.torsoSelection = torsoSelection;
        playerCharacterVisualData.legsSelection = legsSelection;
        playerCharacterVisualData.feetSelection = feetSelection;

        playerCharacterVisualData.hairColor = hairColor;
        playerCharacterVisualData.headColor = headColor;
        playerCharacterVisualData.torsoColor = torsoColor;
        playerCharacterVisualData.legsColor = legsColor;
        playerCharacterVisualData.feetColor = feetColor;
        playerCharacterVisualData.skinColor = skinColor;


        return playerCharacterVisualData;
    }

    #endregion


    public bool ValidateCharacterName()
    {
        if (characaterName== null || characaterName == "")
        {
            // is null or empty...
            return false;
        }
        else
        {
            return true;
        }
    }

    
}

[System.Serializable]
public class PlayerCharacterVisualData
{
    public string characaterName;

    public int hairSelection;
    public int headSelection;
    public int torsoSelection;
    public int legsSelection;
    public int feetSelection;

    public Color hairColor;
    public Color headColor;
    public Color torsoColor;
    public Color legsColor;
    public Color feetColor;
    public Color skinColor;
    public Color eyeColor;

    // default values?
    public PlayerCharacterVisualData()
    {
        characaterName = "Default Character Name";
        hairSelection = 0;
        headSelection = 0;
        torsoSelection = 0;
        legsSelection = 0;
        feetSelection = 0;
        hairColor = Color.magenta;
        headColor = Color.red;
        torsoColor = Color.white;
        legsColor = Color.blue;
        feetColor = Color.black;
        skinColor = Color.gray;
        eyeColor = Color.cyan;

    }
}

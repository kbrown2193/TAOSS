using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualManager : MonoBehaviour
{
    private PlayerCharacterVisualData characterVisualData;

    [SerializeField] SpriteRenderer hairSpriteRenderer;
    [SerializeField] SpriteRenderer headSpriteRenderer;
    [SerializeField] SpriteRenderer torsoSpriteRenderer;
    [SerializeField] SpriteRenderer legsSpriteRenderer;
    [SerializeField] SpriteRenderer feetSpriteRenderer;

    // Duplicated code from player character creator...
    // TODO: make more robust system... is fine for now...
    [SerializeField] private Sprite[] hairSelections;
    [SerializeField] private Sprite[] headSelections;
    [SerializeField] private Sprite[] torsoSelections;
    [SerializeField] private Sprite[] legsSelections;
    [SerializeField] private Sprite[] feetSelections;
    public void SetPlayerCharacterVisualData(PlayerCharacterVisualData newPlayerCharacterVisualData)
    {
        characterVisualData = newPlayerCharacterVisualData;
    }

    public void SetPlayerVisualsFromVisualData(PlayerCharacterVisualData newPlayerCharacterVisualData)
    {
        SetPlayerCharacterVisualData(newPlayerCharacterVisualData);
        SetPlayerVisualsFromVisualDataCurrent();
    }
    public void SetPlayerVisualsFromVisualDataCurrent()
    {
        Debug.Log("Setting Player Visuals fro current data");
        Debug.LogWarning("TODO: make robust...No index checks");
        if (characterVisualData != null)
        {
            // sprites setting
            hairSpriteRenderer.sprite = hairSelections[characterVisualData.hairSelection];
            headSpriteRenderer.sprite = headSelections[characterVisualData.hairSelection];
            torsoSpriteRenderer.sprite = torsoSelections[characterVisualData.hairSelection];
            legsSpriteRenderer.sprite = legsSelections[characterVisualData.hairSelection];
            feetSpriteRenderer.sprite = feetSelections[characterVisualData.hairSelection];

            // color setting
            hairSpriteRenderer.color = characterVisualData.hairColor;
            headSpriteRenderer.color = characterVisualData.headColor;
            torsoSpriteRenderer.color = characterVisualData.torsoColor;
            legsSpriteRenderer.color = characterVisualData.legsColor;
            feetSpriteRenderer.color = characterVisualData.feetColor;
            //.color = characterVisualData.skinColor;
            //.color = characterVisualData.eyeColor;
        }
    }
}

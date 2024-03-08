using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField newGameSaveInputText;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PlayerCharacterCreator playerCharacterCreator;

    // Call this function when the "New Game" button is pressed
    public void NewGameSaveButtonPress()
    {
        string saveName = newGameSaveInputText.text;

        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("Save name cannot be empty.");
            return;
        }

        GameFileHandler gameFileHandler = GameFileHandler.Instance;

        if (!gameFileHandler.CheckIfValidGameName(saveName))
        {
            // If the save already exists, call GameSaveAlreadyExists function
            if (gameFileHandler.SaveExists(saveName))
            {
                GameSaveAlreadyExists();
            }
            else
            {
                // Must be a reserved or invalid name...
                Debug.LogWarning("Reserved or Invalid name attempted");
            }
        }
        else
        {
            // Is valid game name, so...
            // Create a new game save with the provided name
            GameData newGameData = new GameData(saveName); // Initialize game data
            GameManager.Instance.SetReferenceGameData(newGameData);
            gameFileHandler.CreateSave(saveName, newGameData);

            // Open the Character Creator
            Debug.Log("Opening Character Creator");
            playerCharacterCreator.Show();

            Debug.Log("TODO: LOAD LEVEL??? at least in background while player is creating?");
            Debug.Log("TODO: Will start playing cinematic once player create is pressed...");
            //LevelLoader.Instance.BeginLoadingLevel(newGameLevelName);
            // mainMenu.SetMainMenuPage(MainMenu.MainMenuPage.MainMenuFadeOut);
        }
    }

    // This function is called when the game save file already exists
    private void GameSaveAlreadyExists()
    {
        // TODO: Error message Popup
        Debug.LogWarning("A game save with this name already exists.");
    }
}

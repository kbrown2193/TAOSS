using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameMenu : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [SerializeField] private TMP_InputField newGameSaveInputText;
    [SerializeField] private MainMenu mainMenu;

    //private string newGameLevelName = "USKB_01_Demo";

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

        if (gameFileHandler.SaveExists(saveName))
        {
            // If the save already exists, call GameSaveAlreadyExists function
            GameSaveAlreadyExists();
        }
        else
        {
            // Create a new game save with the provided name
            GameData newGameData = new GameData(saveName); // Initialize your game data here
            gameFileHandler.CreateSave(saveName, newGameData);

            // Load the  Level for a new game
            mainMenu.SetMainMenuPage(MainMenu.MainMenuPage.MainMenuFadeOut);


            // 
            Debug.LogWarning("TODO: LOAD LEVEL???");
            //LevelLoader.Instance.BeginLoadingLevel(newGameLevelName);
        }
    }

    // This function is called when the game save file already exists
    private void GameSaveAlreadyExists()
    {
        // TODO: Error message Popup
        Debug.LogWarning("A game save with this name already exists.");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // TODO:  Define the data you want to save here
    public string gameName;
    public string playerName;
    public int lastCheckpoint;

    public PlayerCharacterVisualData playerCharacterVisualData; // save the character info
         

    // Constructor for GameData
    public GameData(string newGameName)
    {
        gameName = newGameName; // this is the name of the save file
        playerName = "Default Player Name";
        lastCheckpoint = 0;

        playerCharacterVisualData = new PlayerCharacterVisualData();
        //characterInfo = new PlayerCharacterVisualData();
    }
}

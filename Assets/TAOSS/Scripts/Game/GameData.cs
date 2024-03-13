using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string gameName;
    public string playerName;
    public int lastCheckpoint;

    public PlayerCharacterVisualData playerCharacterVisualData; // save the character info

    // Campaign Data
    public LifetimeCampaignData lifetimeCampaignData;
    public CampaignData campaignData;

    // Special Data
    public LockData[] gatewaylockDatas = new LockData[4];         

    // Constructor for GameData
    public GameData(string newGameName)
    {
        gameName = newGameName; // this is the name of the save file
        playerName = "Default Player Name";
        lastCheckpoint = 0;

        playerCharacterVisualData = new PlayerCharacterVisualData();
        //characterInfo = new PlayerCharacterVisualData();
        gatewaylockDatas = new LockData[4];
    }
}

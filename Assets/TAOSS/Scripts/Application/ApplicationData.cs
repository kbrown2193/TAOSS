using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplicationData
{
    public string userProfile = "DEFAULT";
    public string lastPlayed = "VERY_FIRST_GAME_01_23_45_67_89"; // last played game name

    // Other settings
    
    // Color scheme?


    // Constructor for ApplicationData
    public ApplicationData(string newUserProfile, string newLastPlayed)
    {
        userProfile = newUserProfile;
        lastPlayed = newLastPlayed;
    }
}

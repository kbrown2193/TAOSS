using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplicationData
{
    public string userProfile = "DEFAULT";
    public string lastPlayed = "VERY_FIRST_GAME_01_23_45_67_89"; // last played game name

    public VideoSettings videoSettings;
    public AudioSettings audioSettings;
    public ControlSettings controlSettings;
    public GameSettings gameSettings;

    // Other settings
    
    // Color scheme?

    // Constructor for ApplicationData
    public ApplicationData(string newUserProfile, string newLastPlayed)
    {
        userProfile = newUserProfile;
        lastPlayed = newLastPlayed;

        // For now...
        // set default settings ?
        videoSettings = new VideoSettings();
        audioSettings = new AudioSettings();
        controlSettings = new ControlSettings();
        gameSettings = new GameSettings();
    }
}

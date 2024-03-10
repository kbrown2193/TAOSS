using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public string playerName;  // the character name created...

    // Player experience progression stats
    public int level;
    public int experience; // if max int? Use overflow
    public int experienceOverflow;

    // Timekeeping stats
    public int secondsPlayed;
    public int fractionalMilliseconds; // how many milliseconds make up the fractional part past the decimal of the seconds played when it is saved... 
    public int timeOverflow; // should always be zero, unless 68.096259734906139015728056823947 of game time years have occured...   
    //public int hoursPlayed;

    // Inventory stats
    public int credits; // credit count (dual purpose as life? or make other stat)
    public int creditsStack; // a stack is = 64? 256? 1024? 4096? 2^?? int.max? credits
    public int creditsStackOverflow; // incase we max a stack, this is the overflow counter;

    // Core Stats
    public int movementSpeed; // a core movement speed, start at 0? is the base, any increase in number is faster movement
    public int healthMax;
    public int healthCurrent; // the current health.

    // start at 0, can increase or decrease over game depending on outcomes etc.
    public int agility; // dexterity? 
    public int charisma;
    public int endurance;
    public int integrity;
    public int knowledge;
    public int strength; // raw power?
    public int willpower;
    public int wisdowm;

    public int unlockedPotential; // start at 0, as game increases, each domain? increase 1, is a "Significant" power increase , maybe be in TechTree stats

    // Quest Stats (see campaign data)

    // Relationship stats

    // TechTree Stats 
    // chosen tech tree progress and current loadout
}

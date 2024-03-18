using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Control
    [SerializeField] private PlayerController playerController;

    // Control - Movement
    public PlayerMovementMode playerMovementMode;

    // Visual
    [SerializeField] private PlayerVisualManager playerVisualManager;

    // Visual - Graphics
    [SerializeField]private PerspectiveScaler perspectiveScaler;

    // Stats
    [SerializeField] private PlayerStats playerStats;

    // Movement
    public bool isMovementEnabled = true;

    public float worldLevelSpeedMultiplier = 1f;

    // Sizing
    public float worldLevelSizeScaler = 1; // be 1 for the most part

    #region Player Control
    // Control Mode Accessors
    public PlayerControlMode GetPlayerControllerControlMode()
    {
        return playerController.GetPlayerControlMode();
    }
    public void SetPlayerControllerControlMode(PlayerControlMode value)
    {
        playerController.SetPlayerControlMode(value);
    }
    public void SetPlayerControllerControlMode(int value)
    {
        playerController.SetPlayerControlMode(value);
    }

    // Refreshers
    public void RefreshPlayerController()
    {
        switch (playerMovementMode)
        {
            case PlayerMovementMode.Platformer2D:
                Debug.LogWarning("Platformer 2D Control...");
                playerController.SetMovementIsEnabled(isMovementEnabled);
                playerController.SetWorldLevelSpeedMultiplier(worldLevelSpeedMultiplier);
                break;
            case PlayerMovementMode.GridPlatformer2D:
                Debug.LogWarning("GridPlatformer 2D Control Not Set Up");
                break;
            case PlayerMovementMode.Platformer3D:
                Debug.LogWarning("Platformer 3D Control Not Set Up");
                break;
            case PlayerMovementMode.GridPlatformer3D:
                Debug.LogWarning("GridPlatformer 3D Control Not Set Up");
                break;
        }
    }
    #endregion

    #region Player Movement
    public void SetPlayerMovementMode(PlayerMovementMode newPlayerMovementMode)
    {
        playerMovementMode = newPlayerMovementMode;
        RefreshPlayerController();
    }   

    public void RepositionPlayer(Vector3 newPosition)
    {
        Debug.Log("Repositioning Player by setting this transform position to " + newPosition);
        this.transform.position = newPosition; // for now set this transform
    }

    public void ReparentPlayer(Transform newParent)
    {
        transform.parent = newParent;
    }

    public void SetIsMovemenEnabled(bool value)
    {
        isMovementEnabled = value;
        RefreshPlayerController();
    }

    public void SetPlayerWorldLevelSpeedMultiplier(float value)
    {
        Debug.Log("Setting Player WorldLevelSpeedMultiplier to " + value);
        worldLevelSpeedMultiplier = value;
        RefreshPlayerController();
    }
    #endregion

    #region Player Scaling
    public void SetPerspectiveScalerIsOn(bool value)
    {
        if (perspectiveScaler != null)
        {
            perspectiveScaler.SetIsOn(value);
        }
        else
        {
            Debug.LogWarning("Null perspective scaler...");
        }
    }

    public void SetWorldLevelSizeScaler(float value)
    {
        worldLevelSizeScaler = value;
        Debug.Log("Setting Player WorldLevel Size Scaler to " + worldLevelSizeScaler);
        if(perspectiveScaler != null)
        {
            perspectiveScaler.SetWorldLevelSizeScaler(worldLevelSizeScaler);
        }
        else
        {
            Debug.LogWarning("Player does not have a perspective player...");
        }
    }
    #endregion

    #region Player Visuals
    public void SetPlayerVisualsFromCharacterData(PlayerCharacterVisualData playerCharacterVisualData)
    {
        if(playerCharacterVisualData != null)
        {
            playerVisualManager.SetPlayerVisualsFromVisualData(playerCharacterVisualData);
        }
        else
        {
            Debug.LogError("Player.SetPlayerVisualsFromCharacterData() playerCharacterVisualData is null!!!");
        }
    }
    #endregion

    #region Player Stats
    public void SavePlayerStats()
    {
        Debug.LogWarning("TODO: Player.SavePlayerStats()");
    }

    public void AddScore(int amount)
    {
        Debug.Log("Adding score to player stats");
        if(playerStats.score + amount < int.MaxValue)
        {
            playerStats.score += amount;
            RefreshScoreVisuals();
        }
        else
        {
            Debug.LogWarning("At Max int for score");
        }
    }

    public void DamagePlayer(int amount)
    {
        Debug.Log("Player Health Prior Damage= " + playerStats.healthCurrent);
        Debug.Log("Damaging player for " + amount);
        if (playerStats.healthCurrent - amount > 0)
        {
            // not dead, damage
            Debug.Log("Damaging player for " + amount);
            playerStats.healthCurrent -= amount;
            RefreshHealthVisuals();
        }
        else
        {
            // dedge
            Debug.LogWarning("Oh dear, you have run out of health");
        }

        Debug.Log("Player Health after Damage= " + playerStats.healthCurrent);
    }

    public void HealPlayer(int amount)
    {
        Debug.Log("Healing player for " + amount);
        if (playerStats.healthCurrent + amount < 100) // revert later, quick fix...
        //if (playerStats.healthCurrent + amount < playerStats.healthMax)
        {
            // can heal
            Debug.Log("Healing playing...");
            playerStats.healthCurrent += amount;
            RefreshHealthVisuals();
        }
        else
        {
            // will be at max
            Debug.Log("Setting player to max health");
            //playerStats.healthCurrent = playerStats.healthMax;
            playerStats.healthCurrent = 100;
            RefreshHealthVisuals();
        }
    }

    // Stat Visuals
    public void RefreshScoreVisuals()
    {
        if(playerStats != null)
        {
            UIManager.Instance.GetPlayerScoreVisualManager().RefreshScoreText(playerStats.score);
        }
        else
        {
            Debug.LogError("Player Stats is null");
        }
    }

    public void RefreshHealthVisuals()
    {
        if (playerStats != null)
        {
            UIManager.Instance.GetPlayerHealthVisualManager().RefreshHealthText(playerStats.healthCurrent);
        }
        else
        {
            Debug.LogError("Player Stats is null");
        }
    }
    #endregion

    #region Currency
    public void AddCredits(int amount)
    {
        if (playerStats.credits + amount < int.MaxValue)
        {
            Debug.Log("Adding Credots");
            playerStats.credits += amount;
        }
        else
        {
            Debug.LogError("Credits Overflow");
        }
    }
    public void SubtractCredits(int amount)
    {
        if (CanSubtractCredits(amount))
        {
            playerStats.credits -= amount;
        }
        else
        {
            Debug.Log("Cannot subract, would result in negative balance");
        }
    }
    public bool CanSubtractCredits(int amount)
    {
        if ((playerStats.credits - amount) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public int GetCredits()
    {
        return playerStats.credits;
    }
    public int SetCredits(int amount)
    {
        return playerStats.credits = amount;
    }
    public void AddArcadeRewardTickets(int amount)
    {
        if(playerStats.arcadeRewardTickets + amount < int.MaxValue)
        {
            Debug.Log("Adding Arcade Reward Tickets");
            playerStats.arcadeRewardTickets += amount;
        }
        else
        {
            Debug.LogError("Arcade Rewards Ticket Overflow");
        }
    }
    public void SubtractArcadeRewardTickets(int amount)
    {
        if (CanSubtractArcadeRewardTickets(amount))
        {
            playerStats.arcadeRewardTickets -= amount;
        }
        else
        {
            Debug.Log("Cannot subract, would result in negative balance");
        }
    }
    public bool CanSubtractArcadeRewardTickets(int amount)
    {
        if ((playerStats.arcadeRewardTickets - amount) >= 0)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }
    public int GetArcadeRewardTicketAmount()
    {
        return playerStats.arcadeRewardTickets;
    }
    public void SetArcadeRewardTicketAmount(int amount)
    {
        playerStats.arcadeRewardTickets = amount;
    }
    #endregion
}

// SAME AS PlatformerMovement mode, but without requiring needing more info like rigid body or orientation?
[System.Serializable]
public enum PlayerMovementMode
{
    Platformer2D,
    GridPlatformer2D, // restricted to moving in cells, for turn based?
    Platformer3D,
    GridPlatformer3D,
}

// TODO: Utilize... implement and animator state machine / blend tree
[System.Serializable]
public enum PlayerDisplayState
{
    Idle, // the animator state machine states? or blend tree states
    Walking,
    Running,
    Jumping,
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Movement
    public PlayerMovementMode playerMovementMode;
    [SerializeField] private PlayerPlatformerMovement playerPlatformerMovement;

    // Visual
    [SerializeField] private PlayerVisualManager playerVisualManager;

    // Visual - Graphics
    [SerializeField]private PerspectiveScaler perspectiveScaler;

    // stats
    [SerializeField] private PlayerStats playerStats;

    // Movement
    public bool isMovementEnabled = true;

    public float worldLevelSpeedMultiplier = 1f;

    // Sizing
    public float worldLevelSizeScaler = 1; // be 1 for the most part..
    // grid movement ... todo...

    #region Player Movement
    public void SetPlayerMovementMode(PlayerMovementMode newPlayerMovementMode)
    {
        playerMovementMode = newPlayerMovementMode;
        RefreshPlayerControllers();
    }

    public void RefreshPlayerControllers()
    {
        switch (playerMovementMode)
        {
            case PlayerMovementMode.Platformer2D:
                // main use case for now...
                playerPlatformerMovement.SetIsEnabled(isMovementEnabled);
                playerPlatformerMovement.SetWorldLevelSpeedMultiplier(worldLevelSpeedMultiplier);
                break;
            case PlayerMovementMode.GridPlatformer2D:
                break;
            case PlayerMovementMode.Platformer3D:
                break;
            case PlayerMovementMode.GridPlatformer3D:
                break;
        }
    }
    public void RepositionPlayer(Vector3 newPosition)
    {
        this.transform.position = newPosition; // for now set this transform
    }

    public void ReparentPlayer(Transform newParent)
    {
        transform.parent = newParent;
    }

    public void SetIsMovemenEnabled(bool value)
    {
        isMovementEnabled = value;
        RefreshPlayerControllers();
    }

    public void SetPlayerWorldLevelSpeedMultiplier(float value)
    {
        Debug.Log("Setting Player WorldLevelSpeedMultiplier to " + value);
        worldLevelSpeedMultiplier = value;
        RefreshPlayerControllers();
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
}

[System.Serializable]
public enum PlayerMovementMode
{
    Platformer2D,
    GridPlatformer2D, // restricted to moving in cells, for turn based?
    Platformer3D,
    GridPlatformer3D,
}

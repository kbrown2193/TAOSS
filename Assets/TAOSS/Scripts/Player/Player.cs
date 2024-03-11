using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovementMode playerMovementMode;
    [SerializeField] private PlayerPlatformerMovement playerPlatformerMovement;

    [SerializeField]private PerspectiveScaler perspectiveScaler;

    public bool isMovementEnabled = true;

    public float worldLevelSpeedMultiplier = 1f;

    public float worldLevelSizeScaler = 1; // be 1 for the most part..
    // grid movement ... todo..

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
}

[System.Serializable]
public enum PlayerMovementMode
{
    Platformer2D,
    GridPlatformer2D, // restricted to moving in cells, for turn based?
    Platformer3D,
    GridPlatformer3D,
}

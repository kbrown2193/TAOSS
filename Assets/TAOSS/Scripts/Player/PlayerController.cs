using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the player input, movement, and actions
/// Should be linked in the player class
/// 
/// for now, put movement scripts here
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerPlatformerMovement playerPlatformerMovement;

    private PlayerControlMode playerControlMode = PlayerControlMode.Disabled; // This mode controls what will be triggered when input is detected
    private PlatformerMovementMode platformerMovementMode = PlatformerMovementMode.IsoPlatformer2DRB;

    private bool isMovementEnabled; // is a variable on player atm also...

    #region Control Mode
    public PlayerControlMode GetPlayerControlMode()
    {
        return playerControlMode;
    }
    public void SetPlayerControlMode(PlayerControlMode value)
    {
        Debug.Log("Setting Player Control Mode to " + value);
        this.playerControlMode = value;
    }
    public void SetPlayerControlMode(int value)
    {
        Debug.Log("Setting Player Control Mode to " + value);
        this.playerControlMode = (PlayerControlMode)value;
    }
    #endregion

    #region Movement - Adventure / Platformer
    public void SetMovementIsEnabled(bool value)
    {
        isMovementEnabled = value; // local variable to isMovementEnabled
        playerPlatformerMovement.SetIsEnabled(value); // movement controller variable setting

    }
    public void SetWorldLevelSpeedMultiplier(float amount)
    {
        playerPlatformerMovement.SetWorldLevelSpeedMultiplier(amount);
    }
    #endregion

    #region Actions
    void OnPrimaryAction()
    {
        Debug.Log("Primary Action"); // Mouse 1 / Gamepad.RightTrigger
        switch(playerControlMode)
        {
            case PlayerControlMode.Disabled:
                Debug.Log("Primary Action - Disabled");
                break;
            case PlayerControlMode.MainMenu:
                Debug.Log("Primary Action - MainMenu");
                break;
            case PlayerControlMode.PauseMenu:
                Debug.Log("Primary Action - PauseMenu");
                break;
            case PlayerControlMode.InGame:
                Debug.Log("Primary Action - InGame");
                break;
            default:
                Debug.Log("Primary Action - Default");
                break;
        }
    }
    void OnSecondaryAction()
    {
        Debug.Log("Seconday Action"); // Mouse 2 / Gamepad.???
        switch (playerControlMode)
        {
            case PlayerControlMode.Disabled:
                Debug.Log("Seconday Action - Disabled");
                break;
            case PlayerControlMode.MainMenu:
                Debug.Log("Seconday Action - MainMenu");
                break;
            case PlayerControlMode.PauseMenu:
                Debug.Log("Seconday Action - PauseMenu");
                break;
            case PlayerControlMode.InGame:
                Debug.Log("Seconday Action - InGame");
                break;
            default:
                Debug.Log("Seconday Action - Default");
                break;
        }
    }
    void OnTertiaryAction()
    {
        Debug.Log("Tertiary Action"); // Mouse 3 ?? / Gamepad.???
        switch (playerControlMode)
        {
            case PlayerControlMode.Disabled:
                Debug.Log("Tertiary Action - Disabled");
                break;
            case PlayerControlMode.MainMenu:
                Debug.Log("Tertiary Action - MainMenu");
                break;
            case PlayerControlMode.PauseMenu:
                Debug.Log("Tertiary Action - PauseMenu");
                break;
            case PlayerControlMode.InGame:
                Debug.Log("Tertiary Action - InGame");
                break;
            default:
                Debug.Log("Tertiary Action - Default");
                break;
        }
    }
    #endregion
}

[System.Serializable]
public enum PlayerControlMode
{
    Disabled,
    MainMenu, // which context should we use?.. main menus?
    PauseMenu, // pause menu navigation
    InGame, // In game... use ?
    Platformer2D,
    GridPlatformer2D, // restricted to moving in cells, for turn based?
    Platformer3D,
    GridPlatformer3D,
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenusInput : MonoBehaviour
{
    public void OnMenus()
    {
        Debug.Log("Menus button pressed");
        if (GameManager.Instance.GameState == GameState.InGame)
        {
            Debug.Log("In game, open pause menu");
            GameManager.Instance.PauseGame();
        }
        else if(GameManager.Instance.GameState == GameState.Paused)
        {
            Debug.Log("Paused, Close pause menu");
            GameManager.Instance.UnPauseGame();
        }
        else
        {
            Debug.Log("Not in a state to open or close pause menu");
        }
    }
}

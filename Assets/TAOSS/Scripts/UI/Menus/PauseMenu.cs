using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject gameObjectToggleObject;

    public void Show()
    {
        Debug.Log("Showing PauseMenu");
        gameObjectToggleObject.SetActive(true);
    }
    public void Hide()
    {
        Debug.Log("Hiding PauseMenu");
        gameObjectToggleObject.SetActive(false);
    }

    public void UnPauseButtonPress()
    {
        GameManager.Instance.UnPauseGame();
    }

    public void ExitToMainMenuButtonPress()
    {
        GameManager.Instance.ExitToMainMenu();
    }

    public void ExitGameButtonPress()
    {
        GameManager.Instance.ExitGame();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private PlayerScoreVisualManager playerScoreVisualManager;
    [SerializeField] private PlayerHealthVisualManager playerHealthVisualManager;

    #region Singleton
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject("UIManager");
                    instance = singleton.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // null component check inits
        if(mainMenu == null)
        {
            Debug.Log("Attempting find main menu on child");
            mainMenu = (MainMenu)gameObject.GetComponentInChildren(typeof(MainMenu));
        }
        if (mainMenu == null)
        {
            Debug.LogError("Did NOT find main memnu");
        }
        if (pauseMenu == null)
        {
            Debug.Log("Attempting find pause menu on child");
            pauseMenu = (PauseMenu)gameObject.GetComponentInChildren(typeof(PauseMenu));
        }
        if (pauseMenu == null)
        {
            Debug.LogError("Did Not find pause memnu");
        }
        if (settingsMenu == null)
        {
            Debug.Log("Attempting find settings menu on child");
            settingsMenu = (SettingsMenu)gameObject.GetComponentInChildren(typeof(SettingsMenu));
        }
        if (settingsMenu == null)
        {
            Debug.LogError("Did Not find settings memnu");
        }
    }
    #endregion

    public MainMenu GetMainMenu()
    {
        return mainMenu;
    }
    public PauseMenu GetPauseMenu()
    {
        return pauseMenu;
    }
    public SettingsMenu GetSettingsMenu()
    {
        return settingsMenu;
    }
    public PlayerScoreVisualManager GetPlayerScoreVisualManager()
    {
        return playerScoreVisualManager;
    }
    public PlayerHealthVisualManager GetPlayerHealthVisualManager()
    {
        return playerHealthVisualManager;
    }
}

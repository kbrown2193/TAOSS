using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PauseMenu pauseMenu;

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

        // ohter inits
        if(mainMenu == null)
        {
            Debug.Log("Attempting find main menu on child");
            mainMenu = (MainMenu)gameObject.GetComponentInChildren(typeof(MainMenu));
        }
        if (mainMenu == null)
        {
            Debug.LogError("Did Not Find main memnu");
        }
        if (pauseMenu == null)
        {
            Debug.Log("Attempting find pasue menu on child");
            pauseMenu = (PauseMenu)gameObject.GetComponentInChildren(typeof(PauseMenu));
        }
        if (pauseMenu == null)
        {
            Debug.LogError("Did Not Find pause memnu");
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    //[SerializeField] private Animator animator;
    [SerializeField] private GameObject gameSaveButtonPrefab;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private TextMeshProUGUI currentSelectedGameSaveText;

    private List<GameObject> gameSaveButtons = new List<GameObject>();
    private float buttonHeight = 24f; // Adjust this value to control the spacing for the first button
    private float buttonSpacing = 10f; // Adjust this value to control the spacing between buttons

    private string currentSelectedGameSave; // Store the currently selected game save

    // Call this function to generate game save buttons in the ScrollView
    public void GenerateGameSaveScrollViewButtons()
    {
        // Clear existing buttons before generating new ones
        ClearGameSaveScrollView();

        // Get all save names from the GameSaveManager
        string[] saveNames = GameFileHandler.Instance.GetAllSaveNames();

        float currentY = -buttonHeight; // Initialize the Y position for the first button

        foreach (string saveName in saveNames)
        {
            // Instantiate a new game save button from the prefab
            GameObject buttonGO = Instantiate(gameSaveButtonPrefab, scrollView.content);

            // Set the text of the button to the game save name
            TextMeshProUGUI buttonText = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = saveName;
            }

            // Set the button's position within the ScrollView
            RectTransform buttonRect = buttonGO.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(0f, currentY);

            // Update the current Y position for the next button
            currentY -= buttonRect.rect.height + buttonSpacing;

            // Add an onClick event to the button to set the selected game save
            Button buttonComponent = buttonGO.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => SetCurrentGameSave(saveName));
            }

            // Add the button to the list for later reference
            gameSaveButtons.Add(buttonGO);
        }

        // Adjust the content size of the ScrollView to fit all buttons
        RectTransform contentRect = scrollView.content.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, -currentY);
    }

    // Call this function to clear all buttons in the ScrollView
    public void ClearGameSaveScrollView()
    {
        foreach (GameObject button in gameSaveButtons)
        {
            Destroy(button);
        }
        gameSaveButtons.Clear();
    }

    // Set the currently selected game save
    private void SetCurrentGameSave(string saveName)
    {
        currentSelectedGameSave = saveName;
        Debug.Log("Selected game save: " + currentSelectedGameSave);

        // Update the TextMeshProUGUI component
        if (currentSelectedGameSaveText != null)
        {
            currentSelectedGameSaveText.text = "Selected Game Save: " + currentSelectedGameSave;
        }
    }

    // Load Game if applicable
    public void LoadGameButtonPress()
    {
        // Loading logic
        if (!string.IsNullOrEmpty(currentSelectedGameSave))
        {
            // Load the selected game save
            GameFileHandler.Instance.LoadSave(currentSelectedGameSave); 
            Debug.Log("Loading game save: " + currentSelectedGameSave);
            Debug.Log("TODO: load to currrent checkpoint");
        }
        else
        {
            Debug.LogError("No game save selected.");
        }
    }

    // Delete the currently selected game save if applicable
    public void DeleteGameButtonPress()
    {
        if (!string.IsNullOrEmpty(currentSelectedGameSave))
        {
            // Call the GameSaveManager's DeleteSave function
            GameFileHandler.Instance.DeleteSave(currentSelectedGameSave);
            Debug.Log("Deleted game save: " + currentSelectedGameSave);

            // Clear the currently selected game save
            currentSelectedGameSave = null;

            // Update the TextMeshProUGUI component
            if (currentSelectedGameSaveText != null)
            {
                currentSelectedGameSaveText.text = "Selected Game Save: ";
            }

            // Rebuild the scroll view
            GenerateGameSaveScrollViewButtons();
        }
        else
        {
            Debug.LogError("No game save selected to delete.");
        }
    }
}

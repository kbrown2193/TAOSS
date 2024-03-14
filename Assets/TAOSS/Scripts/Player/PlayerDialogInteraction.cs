using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDialogInteraction : MonoBehaviour
{
    public int currentResponseChoice;
    // Start is called before the first frame update
    void Update()
    {
        // TODO CHANGE TO USING INPUT SYSTEM
        //Debug.LogWarning("Warning using GetKeyDown... upgrade to input system actions");
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDialogInteract();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            IncreaseResponseChoice();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DecreaseResponseChoice();
        }

    }


    private void PlayerDialogInteract()
    {
        Debug.Log("Player Interacted with E?");
        DialogManager.Instance.PlayerDialogInteract();
        //movementInputDirection = value.Get<Vector2>();
    }

    public void SetResponseChoice(int newResponseChoice)
    {
        currentResponseChoice = newResponseChoice;
        DialogManager.Instance.SetHoveredResponse(newResponseChoice);
    }

    public void IncreaseResponseChoice()
    {
        if(DialogManager.Instance.IncreaseHoveredResponse())
        {
            //DialogManager.Instance.IncreaseHoveredResponse();
            currentResponseChoice++;
        }
    }
    public void DecreaseResponseChoice()
    {
        if (DialogManager.Instance.DeceaseHoveredResponse())
        {
            //DialogManager.Instance.IncreaseHoveredResponse();
            currentResponseChoice--;
        }
    }
}

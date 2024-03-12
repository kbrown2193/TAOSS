using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogBox : MonoBehaviour
{
    [SerializeField] Image dialogBoxBackgroundImage;
    [SerializeField] TMP_Text dialogText;

    #region Typing
    public void TypeText(string newText)
    {
        //dialogText.text = ""; // clear next?
        // for now make it a simple text assignment.. later add typing coroutine...
        dialogText.text = newText;
    }
    #endregion

    #region Visiblity
    /// <summary>
    /// Later can make it more smooth transition...
    /// </summary>
    public void Show()
    {
        dialogBoxBackgroundImage.enabled = true;
        dialogText.enabled = true;
    }

    public void Hide()
    {
        dialogBoxBackgroundImage.enabled = false;
        dialogText.enabled = false;
    }
    #endregion

    #region Color and Formatting
    public void SetDialogTextFontSize(float size)
    {
        dialogText.fontSize = size;
    }
    public void SetDialogTextColor(Color newColor)
    {
        dialogText.color = newColor;
    }
    /// <summary>
    /// Sets the background image to this color
    /// </summary>
    /// <param name="newColor"></param>
    public void SetDialogBoxColor(Color newColor)
    {
        dialogBoxBackgroundImage.color = newColor;
    }
    #endregion
}

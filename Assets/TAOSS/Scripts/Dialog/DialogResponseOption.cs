using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogResponseOption : MonoBehaviour
{
    [SerializeField] private Button responseButton;
    [SerializeField] private TMP_Text responseText;

    [SerializeField] private int responseIndex; // which index this pertains to in the array of responses.  the value to output

    #region Typing
    public void TypeResponseText(string newText)
    {
        //responseText.text = ""; // clear next?
        // for now make it a simple text assignment.. later add typing coroutine...
        responseText.text = newText;
    }
    #endregion

    #region Visiblity
    /// <summary>
    /// Later can make it more smooth transition...
    /// </summary>
    public void Show()
    {
        responseButton.image.enabled = true;
        responseButton.enabled = true;
        responseText.enabled = true;
    }

    public void Hide()
    {
        responseButton.image.enabled = false;
        responseButton.enabled = false;
        responseText.enabled = false;
    }
    #endregion

    #region On Click Actions
    public void ResponseClicked()
    {
        // attach to button prior... then...
        Debug.Log("Response = " + responseIndex);
    }
    #endregion

    #region Response Related
    public void SetResponseIndex(int newIndex)
    {
        responseIndex = newIndex;
    }
    public int GetResponseIndex()
    { 
        return responseIndex;
    }
    #endregion

    #region Colors and Formatting
    public void SetResponseTextFontSize(float size)
    {
        responseText.fontSize = size;
    }
    public void SetResponsTextColor(Color newColor)
    {
        responseText.color = newColor;
    }
    /// <summary>
    /// Sets the button image to this color
    /// </summary>
    /// <param name="newColor"></param>
    public void SetResponseButtonColor(Color newColor)
    {
        responseButton.image.color = newColor;
    }
    #endregion
}

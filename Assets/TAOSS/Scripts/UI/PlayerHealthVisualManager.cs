using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthVisualManager : MonoBehaviour
{
    public TMP_Text healthText;

    public void RefreshHealthText(int amount)
    {
        healthText.text = amount.ToString();
    }
}

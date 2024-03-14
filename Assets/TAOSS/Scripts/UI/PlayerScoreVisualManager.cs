using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreVisualManager : MonoBehaviour
{
    public TMP_Text scoreText;

    public void RefreshScoreText(int amount)
    {
        scoreText.text = amount.ToString(); 
    }
}

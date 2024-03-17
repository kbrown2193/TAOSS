using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the sprites that will be recalled from digit sprites
/// </summary>
public class DigitSpriteManager : MonoBehaviour
{
    [SerializeField] Sprite[] digitSprites = new Sprite[10];

    public Sprite GetDigitSprite(Digit digit)
    {
        return digitSprites[(int)digit];
    }
    public Sprite GetDigitSprite(int digit)
    {
        return digitSprites[digit];
    }
}

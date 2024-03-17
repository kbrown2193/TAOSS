using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pricetag : MonoBehaviour
{
    [SerializeField] private DigitSprite[] digitSprites;
    [SerializeField] private SpriteRenderer pricetagSpriteRenderer;  // the card or actual object... the background to the digit sprites
    [SerializeField] private int pricetagAmount;

    void Awake()
    {
        SetDigitSpritesFromAmount(pricetagAmount);
    }

    void OnEnterWorldLevelLocal()
    {
        // set new amount? based on difficulty ? and recent events / favor
        //TODO: ...
        SetDigitSpritesFromAmount(pricetagAmount);
    }

    public void SetDigitSpritesFromAmount(int amount)
    {
        if (digitSprites != null)
        {
            if (pricetagAmount < 10 && pricetagAmount >= 0)
            {
                // 1 digit
                Debug.Log("1 digit pricetag");
                digitSprites[0].enabled = true;
                digitSprites[1].enabled = false;
                digitSprites[2].enabled = false;
                digitSprites[1].enabled = false;
                digitSprites[2].enabled = false;
                digitSprites[0].SetDigit(amount);
                digitSprites[0].Show();
                digitSprites[1].Hide();
                digitSprites[2].Hide();
            }
            else if (pricetagAmount < 100 && pricetagAmount >= 10)
            {
                // 2 digits
                // if needed to be center, would need to move sprites...
                Debug.Log("2 digit pricetag");
                int amountDigit0 = amount % 10;
                int amountDigit1 = (amount / 10); //  Floor( amount / 10 ) 

                digitSprites[0].enabled = true;
                digitSprites[1].enabled = true;
                digitSprites[2].enabled = false;
                digitSprites[0].SetDigit(amountDigit0);
                digitSprites[1].SetDigit(amountDigit1);
                digitSprites[0].Show();
                digitSprites[1].Show();
                digitSprites[2].Hide();
            }
            else if (pricetagAmount < 1000 && pricetagAmount >= 100)
            {
                // 3 digits
                Debug.Log("3 digit pricetag"); // if n = count = 3 ...

                int amountDigit0 = amount % 100; // n-3 = % 100
                int amountDigit1 = (amount % 10); //  Floor( amount %10 )  // n-2 is % 10
                int amountDigit2 = (amount / 100); //  Floor( amount /10 )  // n -1 is /10

                digitSprites[0].enabled = true;
                digitSprites[1].enabled = true;
                digitSprites[2].enabled = true;
                digitSprites[0].SetDigit(amountDigit0);
                digitSprites[1].SetDigit(amountDigit1);
                digitSprites[2].SetDigit(amountDigit2);
                digitSprites[0].Show();
                digitSprites[1].Show();
                digitSprites[2].Show();
            }
            else
            {
                Debug.LogWarning("Invalid pricetag amount");
            }
        }
        else
        {
            Debug.LogError("Digit sprites are null");
        }
    }

    private void SetDigitSpriteValue(int index, int digit)
    {
        if (index < digitSprites.Length)
        {
            digitSprites[index].SetDigit(digit);
        }
        else
        {
            Debug.LogError("Invalid index given");
        }
    }
    private void SetDigitSpriteValue(int index, Digit digit)
    {
        if (index < digitSprites.Length)
        {
            digitSprites[index].SetDigit(digit);
        }
        else
        {
            Debug.LogError("Invalid index given");
        }
    }

    public void SetPriceTag(int value)
    {
        Debug.Log("Setting Pricetag to " + value);
        SetDigitSpritesFromAmount(value);
    }

    /// <summary>
    ///  This is to set the base background sprite
    /// </summary>
    /// <param name="sprite"></param>
    public void SetPriceTagSprite(Sprite sprite)
    {
        pricetagSpriteRenderer.sprite = sprite;
    }
}

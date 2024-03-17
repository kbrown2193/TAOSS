using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigitSprite : MonoBehaviour
{
    [SerializeField] private DigitSpriteManager digitSpriteManager; // where to look for the sprites

    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if(spriteRenderer == null)
            {
                Debug.LogError("No sprite renderer component found");
            }
        }
    }

    public void SetDigit(Digit digit)
    {
        //Debug.Log("setting digit to " + digit);
        spriteRenderer.sprite = digitSpriteManager.GetDigitSprite(digit);
    }
    public void SetDigit(int digit)
    {
        //Debug.Log("setting digit to " + digit);
        spriteRenderer.sprite = digitSpriteManager.GetDigitSprite(digit);
    }
    public void Show()
    {
        spriteRenderer.gameObject.SetActive(true);
    }
    public void Hide()
    {
        spriteRenderer.gameObject.SetActive(false);
    }
}

public enum Digit
{
    Zero,
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
}

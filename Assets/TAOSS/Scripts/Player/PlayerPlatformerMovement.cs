using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerMovement : MonoBehaviour
{
    //private Vector2 movement;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float worldLevelSpeedMultiplier = 1f;

    private bool isEnabled;

    private Vector2 movementInputDirection;

    // Rigid body based?
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMovement(InputValue value)
    {
        movementInputDirection = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if(isEnabled)
        {
            rb.velocity = (movementInputDirection * movementSpeed) * worldLevelSpeedMultiplier;
        }
    }

    public void SetWorldLevelSpeedMultiplier(float value)
    {
        Debug.Log("Setting Player Platformer WorldLevelSpeed Multiplier to " + value);

        worldLevelSpeedMultiplier = value;
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    public void RepositionPlayer(Vector3 newPosition)
    {
        this.transform.position = newPosition;
    }

    public void SetIsEnabled(bool value)
    {
        isEnabled = value;
    }
    // Same as GetIsEnabled...
    public bool IsEnabled
    {
        get { return isEnabled; }
    }
}

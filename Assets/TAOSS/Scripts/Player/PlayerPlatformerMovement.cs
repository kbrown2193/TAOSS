using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerMovement : MonoBehaviour
{
    //private Vector2 movement;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float worldLevelSpeedMultiplier = 1f;

    private Vector3 movementPositionLimitsMax = new Vector3(300, 2);
    private Vector3 movementPositionLimitsMin = new Vector3(-300, -100);
    
    private Transform playerPositionTransform;

    private bool isEnabled;

    private Vector2 movementInputDirection;
    private Vector2 appliedForceDirection; // which direction the force will be applied (is clamped by movementPositionLimits)

    // Rigid body based?
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPositionTransform = GetComponent<Transform>(); // for now is transform on this player object...
    }

    private void OnMovement(InputValue value)
    {
        movementInputDirection = value.Get<Vector2>();
    }

    /// <summary>
    /// 2D Platformer Movement logic in Fixed Update
    /// Can clean this up... just get working...
    /// </summary>
    void FixedUpdate()
    {
        if(isEnabled)
        {
            appliedForceDirection = movementInputDirection;

            if (movementInputDirection.y > 0)
            {
                // is moving up
                if (playerPositionTransform.position.y >= movementPositionLimitsMax.y)
                {
                    Debug.LogWarning("At Max Verticality, do not move up...");
                    appliedForceDirection.y = 0;
                }

                if (movementInputDirection.x > 0)
                {
                    // is moving right
                    // RIGHT CHECK
                    if(playerPositionTransform.position.x >= movementPositionLimitsMax.x)
                    {
                        Debug.LogWarning("At Max Horizontalness, do not move right...");
                        appliedForceDirection.x = 0;
                    }
                }
                else
                {
                    // is moving left
                    // LEFT CHECK
                    if (playerPositionTransform.position.x <= movementPositionLimitsMin.x)
                    {
                        Debug.LogWarning("At Min Horizontalness, do not move Left...");
                        appliedForceDirection.x = 0;
                    }
                }
            }
            else
            {
                // is moving down
                if (playerPositionTransform.position.y <= movementPositionLimitsMin.y)
                {
                    Debug.LogWarning("At min Verticality, do not move down...");
                    appliedForceDirection.y = 0;
                }

                if (movementInputDirection.x >0)
                {
                    // is moving right
                    // RIGHT CHECK
                    if (playerPositionTransform.position.x >= movementPositionLimitsMax.x)
                    {
                        Debug.LogWarning("At Max Horizontalness, do not move right...");
                        appliedForceDirection.x = 0;
                    }
                }
                else
                {
                    // is moving left
                    // LEFT CHECK
                    if (playerPositionTransform.position.x <= movementPositionLimitsMin.x)
                    {
                        Debug.LogWarning("At Min Horizontalness, do not move Left...");
                        appliedForceDirection.x = 0;
                    }
                }
            }

            rb.velocity = (appliedForceDirection * movementSpeed) * worldLevelSpeedMultiplier;
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

    /// <summary>
    /// Set the limits to clamp player position to keep contained
    /// </summary>
    /// <param name="lowerLimitXY"> The lower limit for x and y value</param>
    /// <param name="upperLimitXY">the upper limit for x and y value </param>
    public void SetMovementPositionLimits(Vector2 lowerLimitXY, Vector2 upperLimitXY)
    {
        movementPositionLimitsMin = lowerLimitXY;
        movementPositionLimitsMax = upperLimitXY;
    }
}

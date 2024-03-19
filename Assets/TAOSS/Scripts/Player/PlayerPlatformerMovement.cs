using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerMovement : MonoBehaviour
{
    [SerializeField] private Camera camera;

    // does this need to be serialized???
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float worldLevelSpeedMultiplier = 1f;

    [SerializeField] PlatformerMovementMode platformerMovementMode = PlatformerMovementMode.IsoPlatformer2DRB;

    private Vector3 movementPositionLimitsMax = new Vector3(300, 2);
    private Vector3 movementPositionLimitsMin = new Vector3(-300, -100);

    [SerializeField] private Transform playerPositionTransform;
    [SerializeField] private Transform playerAimRotationTransform; // this will be rotated to the facing direction

    private bool isEnabled;

    private Vector2 movementInputDirection;
    private Vector2 appliedForceDirection; // which direction the force will be applied (is clamped by movementPositionLimits)
    private Vector2 mouseInputPosition;
    private Vector2 mousePosition;

    public DirectionFacing directionFacing; // public for now... test...
    private Vector2 facingDirection;
    private float facingAngle;
    private Vector3 facingVector = Vector3.zero;

    // Rigid body based
    private Rigidbody2D rb2D;
    private Rigidbody rb; // 3d rigid body... unused atm

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if(playerPositionTransform == null)
        {
            playerPositionTransform = GetComponent<Transform>(); // for now is transform on this player object...
        }

        if(rb2D == null)
        {
            Debug.LogError("RB2D is null");
        }
        if (playerAimRotationTransform == null)
        {
            Debug.LogError("playerAimRotationTransform is null"); // attach this in field for now...
        }
    }

    private void OnMovement(InputValue value)
    {
        movementInputDirection = value.Get<Vector2>();
    }
    private void OnMousePosition(InputValue value)
    {
        mouseInputPosition = value.Get<Vector2>();
    }

    /// <summary>
    /// 2D Platformer Movement logic in Fixed Update
    /// Can clean this up... just get working...
    /// </summary>
    void FixedUpdate()
    {
        if(isEnabled)
        {
            // calculate the force
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

            // Apply the force
            rb2D.velocity = (appliedForceDirection * movementSpeed) * worldLevelSpeedMultiplier;

            // Calculate the facing direction
            mousePosition = camera.ScreenToWorldPoint(mouseInputPosition); // this gets a world posiition corresponding to the mouse positon
            //Debug.LogWarning("Only calculates RB2D for now..");
            facingDirection = mousePosition - rb2D.position;
            facingAngle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            //facingAngle = (Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg) - 90; // add / subtract corection angle if facing up or down  instead of right as 0 angle
            //facingAngle = (Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg) + 90;
            //Debug.Log("Player Mouse Input Position" + mouseInputPosition.ToString() + ", Mouse Position = " + mousePosition.ToString());
            //Debug.Log("Player facingDirection " + facingDirection.ToString() + ", facingAngle = " + facingAngle.ToString());

            // Apply facing direction? if applicable
            rb2D.MoveRotation(facingAngle); //??
            facingVector.z = facingAngle;
            //playerPositionTransform.rotation = Quaternion.Euler(facingVector); // IF YOU WANT FULL SPINNING OF PLAYER this is fine... 
            // we only want to rotate the aimer portion of player
            playerAimRotationTransform.rotation = Quaternion.Euler(facingVector);
            directionFacing = CalculateDirectionFacingFromAngle(facingAngle);
        }
    }

    public void SetWorldLevelSpeedMultiplier(float value)
    {
        Debug.Log("Setting Player Platformer WorldLevelSpeed Multiplier to " + value);
        worldLevelSpeedMultiplier = value;
    }

    public void ResetVelocity()
    {
        rb2D.velocity = Vector2.zero;
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

    #region Angle and Direction
    /// <summary>
    /// Calculate the direction facing givin an angle from -180 to 180
    /// 
    /// EAST 0 DEGREE SYSTEM (for 2D...)
    /// Assumes 0 is East
    /// 90 is north
    /// -90 is south
    /// 180 is west
    /// -180 is west
    /// 
    /// gonna do a simple if, reductive return path....
    /// could be more elegeant, but this will be simplistic
    /// 
    /// Moving East best performance?, then NE / SE, then N/S, then NW / SW, then last W
    /// </summary>
    /// <param name="angle"></param>
    /// <returns>DirectionFacing</returns>
    public DirectionFacing CalculateDirectionFacingFromAngle(float angle)
    {
        if (angle >= 0)
        {
            // North
            if (angle < 30)
            {
                return DirectionFacing.East;
            }
            else
            {
                // Could be facing north, west, north east, northwest
                if(angle < 60)
                {
                    // this must be north east...
                    return DirectionFacing.NorthEast;
                }
                else
                {
                    // could be north.. northwest... west..
                    if(angle < 120)
                    {
                        return DirectionFacing.North;
                    }
                    else
                    {
                        if (angle < 150)
                        {
                            return DirectionFacing.NorthWest;
                        }
                        else
                        {
                            // Must be west...
                            return DirectionFacing.West;
                        }
                    }
                }
            }
        }
        else
        {
            // South
            if(angle >-30)
            {
                // Facing mainly East, with some small south variation (30 degrees)
                return DirectionFacing.East;
            }
            else
            {
                // Could be facing South, West, SouthEast, SouthWest
                if (angle > -60)
                {
                    // this must be south east...
                    return DirectionFacing.SouthEast;
                }
                else
                {
                    // could be south.. southwest... west..
                    if (angle > -120)
                    {
                        return DirectionFacing.South;
                    }
                    else
                    {
                        if (angle > -150)
                        {
                            return DirectionFacing.SouthWest;
                        }
                        else
                        {
                            // Must be west...
                            return DirectionFacing.West;
                        }
                    }
                }
            }
        }
    }
    #endregion
}

[System.Serializable]
public enum PlatformerMovementMode
{
    IsoPlatformer2D, // Isometric 2D custom movement : move in X and Y direction (Z locked)
    IsoPlatformer2DRB, // Isometric 2D rigid body based : move in X and Y direction (Z locked)
    TopDownPlatformer2D, // Top down view 2D custom movement : move in X and Y direction (Z locked)
    TopDownPlatformer2DRB, // Top down view 2D rigid body based : move in X and Y direction (Z locked)
    GridPlatformer2D, // 2D grid movement : restricted to moving in cells, 2D
    Platformer3D, // 3D platformer custom movement platformer
    Platformer3DRB, // 3D  rigid body based platformer
    GridPlatformer3D, // 3D grids movement : restricted to 3D grid movemnts, 3D
}

/// <summary>
/// Eight direction facing enumeration for character rotation states
/// </summary>
[System.Serializable]
public enum DirectionFacing
{
    East, // east if facing absolute angle is less than 90
    North, // north if facing angle is positive
    West, // if facing absolute angle is greater than 90 (to 180)
    South, //  south if facing angle is negative
    NorthEast,
    NorthWest,
    SouthEast,
    SouthWest,
}

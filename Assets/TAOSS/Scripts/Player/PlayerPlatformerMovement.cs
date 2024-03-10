using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformerMovement : MonoBehaviour
{
    //private Vector2 movement;
    [SerializeField] float movementSpeed = 1f;

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
        rb.velocity = movementInputDirection * movementSpeed;
    }
}

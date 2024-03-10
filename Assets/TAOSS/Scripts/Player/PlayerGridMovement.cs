using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGridMovement : MonoBehaviour
{   
    // TODO: ...
    //private Vector2 movement;
    [SerializeField] float movementSpeed = 1f;

    private Vector2 movementInputDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnMovement(InputValue value)
    {
        movementInputDirection = value.Get<Vector2>();
    }
}

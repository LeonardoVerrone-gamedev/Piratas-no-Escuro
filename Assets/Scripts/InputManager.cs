using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput _PlayerInput;
    private InputAction _MoveAction;
    
    private void Awake()
    {
        _PlayerInput = GetComponent<PlayerInput>();
        _MoveAction = _PlayerInput.actions["Move"];
    }

    private void Update()
    {
        Movement = _MoveAction.ReadValue<Vector2>();
    }
}

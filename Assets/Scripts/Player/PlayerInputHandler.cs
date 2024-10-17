using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;

    private Vector2 _moveInput;

    public Vector2 MoveInput { get => _moveInput; }

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();

        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;
    }


    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}

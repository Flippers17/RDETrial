using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;

    private Vector2 _moveInput;

    public Vector2 MoveInput { get => _moveInput; }

    public UnityAction OnAttack;


    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();

        _input.actions["Move"].performed += OnMoveInput;
        _input.actions["Move"].canceled += OnMoveInput;

        _input.actions["Attack"].performed += OnAttackInput;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMoveInput;
        _input.actions["Move"].canceled -= OnMoveInput;

        _input.actions["Attack"].performed -= OnAttackInput;
    }


    private void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }


    private void OnAttackInput(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.DefaultActions _defaultActions;
    private PlayerMovement _playerMovement;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _defaultActions = _playerInput.Default;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        
        // jump
        _defaultActions.Jump.performed += ctx => StartCoroutine(_playerMovement.JumpBuffer());
    }

    private void FixedUpdate()
    {
        // movement
        _playerMovement.Move(_defaultActions.Movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _defaultActions.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _defaultActions.Disable();
    }
}

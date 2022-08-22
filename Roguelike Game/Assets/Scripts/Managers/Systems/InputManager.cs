using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.DefaultActions _defaultActions;
    private PlayerMovement _playerMovement;
    private PlayerUI _playerUI;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _defaultActions = _playerInput.Default;
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerUI = FindObjectOfType<PlayerUI>();
        
        // jump
        _defaultActions.Jump.performed += ctx => StartCoroutine(_playerMovement.JumpBuffer());
        
        // UI
        _defaultActions.Inventory.performed += ctx => _playerUI.ToggleInventoryUI();
        _defaultActions.CharacterInfo.performed += ctx => _playerUI.ToggleCharacterInfo();
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

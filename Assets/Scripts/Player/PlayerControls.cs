using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private bool _jump = false;
    private bool _shoot = false;

    private Vector2 _aimInput;
    private Vector2 _directionInput;
    
    private Controls _controls;
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    
    private void Awake()
    {
        _controls = new Controls();
        _controls.Player.Move.performed += ctx => { _directionInput = ctx.ReadValue<Vector2>(); };
        _controls.Player.Move.canceled += _ => { _directionInput = Vector2.zero; };
        _controls.Player.Jump.performed += ctx => {  _jump = true; };
        _controls.Player.Jump.canceled += _ => { _jump = false; };

        _controls.Player.Shoot.performed += ctx => { _shoot = true; };
        _controls.Player.Shoot.canceled += _ => { _shoot = false; };
        
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Player.Disable();
    }
    
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void Update()
    {
        if (!PlayerEntity.Instance.frozeControls) _playerCombat.Shoot(_shoot, _aimInput);
    }

    private void FixedUpdate()
    {
        if (!PlayerEntity.Instance.frozeControls) _playerMovement.Move(_directionInput.x, _jump);
    }
}

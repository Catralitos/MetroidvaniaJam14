using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private bool _dash = false;
    private bool _jump = false;
    private bool _shoot = false;

    private Vector2 _aimInput;
    private Vector2 _directionInput;
    
    private Controls _controls;
    private PlayerMovement _playerMovement;
    private PlayerCombat _playerCombat;
    private Vector2 _mousePosition;
    
    private void Awake()
    {
        _controls = new Controls();
        _controls.Player.Move.performed += ctx => { _directionInput = ctx.ReadValue<Vector2>(); };
        _controls.Player.Move.canceled += _ => { _directionInput = Vector2.zero; };
        _controls.Player.Jump.performed += ctx => {  _jump = true; };
        _controls.Player.Jump.canceled += _ => { _jump = false; };
        _controls.Player.Dash.performed += _ => { _dash = true; };
        _controls.Player.Dash.canceled += _ => { _dash = false; };

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
        _mousePosition = Mouse.current.position.ReadValue();
        _aimInput = (_mousePosition - (Vector2) (transform.position)).normalized;
        if (!PlayerEntity.Instance.frozeControls) _playerCombat.Shoot(_shoot, _aimInput);
    }

    private void FixedUpdate()
    {
        if (!PlayerEntity.Instance.unlockedDash) _dash = false;
        if (!PlayerEntity.Instance.frozeControls) _playerMovement.Move(_directionInput.x, _directionInput.y, _jump, _dash);
    }
}

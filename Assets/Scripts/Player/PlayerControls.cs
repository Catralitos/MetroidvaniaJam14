using Cinemachine;
using GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerControls : MonoBehaviour
    {
        private bool _dash = false;
        private bool _jump = false;
        private bool _kick = false;
        private bool _shoot = false;
        private bool _pause = false;

        private int _unpausedFrames;
        private int _pausedFrames;

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
            _controls.Player.Jump.performed += ctx => { _jump = true; };
            _controls.Player.Jump.canceled += _ => { _jump = false; };
            _controls.Player.Dash.performed += _ => { _dash = true; };
            _controls.Player.Dash.canceled += _ => { _dash = false; };
            _controls.Player.Melee.performed += _ => { _kick = true; };
            _controls.Player.Melee.canceled += _ => { _kick = false; };
            _controls.Player.Shoot.performed += ctx => { _shoot = true; };
            _controls.Player.Shoot.canceled += _ => { _shoot = false; };
            _controls.Player.Pause.performed += _ => { _pause = true; };
            _controls.Player.Pause.canceled += _ => { _pause = false; };
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
            if (PlayerEntity.Instance.dying) return;
            if (!LevelManager.Instance.gameIsPaused)
            {
                _unpausedFrames++;
            }
            if (_pause && LevelManager.Instance.gameIsPaused && _pausedFrames > 0)
            {
                LevelManager.Instance.UnpauseGame();
                _pausedFrames = 0;
            }
            else if (_pause && !LevelManager.Instance.gameIsPaused && _unpausedFrames > 20)
            {
                LevelManager.Instance.PauseGame();
                _unpausedFrames = 0;
            }
            else if (!_pause && LevelManager.Instance.gameIsPaused)
            {
                _pausedFrames++;
            }
       

            if (PlayerEntity.Instance.displayingTooltip && PlayerEntity.Instance.UI.canCancelTooltip && _shoot)
            {
                PlayerEntity.Instance.UI.CloseTooltip();
            }

            _mousePosition = Mouse.current.position.ReadValue();

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.localPosition);

            _aimInput = ((Vector2) _mousePosition - (Vector2) screenPosition).normalized;
            if (!PlayerEntity.Instance.frozeControls) _playerCombat.Shoot(_shoot, _aimInput);
            if (!PlayerEntity.Instance.frozeControls) _playerCombat.Kick(_kick);
        }

        private void FixedUpdate()
        {
            if (PlayerEntity.Instance.dying) return;
            if (!PlayerEntity.Instance.unlockedDash) _dash = false;
            if (!PlayerEntity.Instance.frozeControls)
                _playerMovement.Move(_directionInput.x, _directionInput.y, _jump, _dash);
        }
    }
}
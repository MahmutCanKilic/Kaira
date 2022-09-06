using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    public PlayerControls _playerControls;
    private PlayerHandler _player;

    [Header("Variables")]
    [SerializeField] private float _inputHoldTime = 0.2f;
    private float _jumpInputStartTime;

    [Header("Movement Inputs")] //sonra private yap getters setters ekle
    public bool isMoving;
    public Vector2 movementInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    [Header("Jump Variables")]
    [SerializeField] private bool _isJumpPressed;
    [SerializeField] private bool _isJumpReleased;
    public bool IsJumpPressed { get { return _isJumpPressed; } }
    public bool IsJumpReleased { get { return _isJumpReleased; } }

    [Header("Interactable F key vs")]
    public bool f_Key_Press;
    public bool f_Key_Release;

    [Header("Dash Variables")]
    [SerializeField] private bool _isDashPressed;
    private float _dashInputStartTime;

    public bool IsDashPressed { get { return _isDashPressed; } }

    [Header("Attack Variables")]
    [SerializeField] private bool _primaryAttack;
    [SerializeField] private bool _secondaryAttack;

    public bool PrimaryAttack { get { return _primaryAttack; } }
    public bool SecondaryAttack { get { return _secondaryAttack; } }


    [Header("Weapon Variables")]
    [SerializeField] private bool _isWeaponUp;
    [SerializeField] private bool _isWeaponDown;

    public bool IsWeaponUp { get { return _isWeaponUp; } }
    public bool IsWeaponDown { get { return _isWeaponDown; } }

    private void Awake()
    {
        _player = GetComponent<PlayerHandler>();

        if (_playerControls == null)
        {
            _playerControls = new PlayerControls();

            _playerControls.PlayerMovement.Movement.performed += OnMovementInput;
            _playerControls.PlayerActions.Jump.started += OnJumpInput;
            _playerControls.PlayerActions.Dash.started += OnDashInput;            
        }
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputTime();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleIntreaction();
        HandleJumpInput();
        HandleDashInput();
        HandleAttackInputs();
    }

    #region Interaction Input

    private void HandleIntreaction()
    {
        f_Key_Press = _playerControls.PlayerActions.Interact.WasPressedThisFrame();
        f_Key_Release = _playerControls.PlayerActions.Interact.WasReleasedThisFrame();
    }

    #endregion

    #region Movement Input

    void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _player.Core.Movement.IsMovementPressed = horizontalInput != 0; // || verticalInput != 0;
    }

    #endregion

    #region Jump Input

    void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _jumpInputStartTime = Time.time;
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            _isJumpPressed = false;
        }
    }

    private void HandleJumpInput()
    {
        _isJumpPressed = _playerControls.PlayerActions.Jump.WasPressedThisFrame();
        _isJumpReleased = _playerControls.PlayerActions.Jump.WasReleasedThisFrame();
    }

    #endregion

    #region Dash Input

    void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _dashInputStartTime = Time.time;
        }
    }

    private void HandleDashInput()
    {
        _isDashPressed = _playerControls.PlayerActions.Dash.WasPressedThisFrame();
    }

    private void CheckDashInputTime()
    {
        if (Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            _isDashPressed = false;
        }
    }

    #endregion

    #region Weapon Inputs

    void HandleAttackInputs()
    {
        _isWeaponUp = _playerControls.PlayerActions.ChangeWeaponUp.WasPressedThisFrame();
        _isWeaponDown = _playerControls.PlayerActions.ChangeWeaponDown.WasPressedThisFrame();
        _primaryAttack = _playerControls.PlayerActions.PrimaryAttack.WasPressedThisFrame();
        _secondaryAttack= _playerControls.PlayerActions.SecondaryAttack.WasPressedThisFrame();
    }

    #endregion

    #region enable / Disable

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    #endregion
}

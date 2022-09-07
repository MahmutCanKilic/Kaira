using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Movement : CoreComponents //IImpactable //for jumppads
{
    public Rigidbody2D Rigidbody { get; set; }

    #region Movement Variables with getters and setters

    //player input values
    [Header("Movement Variables")]
    private bool _facingRight = true;
    [SerializeField] private Vector2 _workspace;
    [SerializeField] private bool _canMove = true;
    [SerializeField] private bool _canRotate = true;
    [SerializeField] private bool _isMovementPressed;
    [SerializeField] private float _currentSpeed = 1;
    [SerializeField] private float _walkSpeed = 3;
    [SerializeField] private float _sprintSpeed = 6;
    public Vector2 CurrentVelocity { get; private set; }

    #region getters setters

    public Vector2 Workspace { get { return _workspace; } set { _workspace = value; } }
    public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
    public float WalkSpeed { get { return _walkSpeed; } }
    public float SprintSpeed { get { return _sprintSpeed; } } //set gerek yok gibi
    public bool IsMovementPressed { get { return _isMovementPressed; } set { _isMovementPressed = value; } }
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    public bool CanRotate { get { return _canRotate; } set { _canRotate = value; } }

    #endregion

    #endregion

    #region Gravity And Grounded Variables

    [Header("Grounded")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _groundedGravity = -0.05f;
    [SerializeField] private float _groundRad = 0.1f;
    [SerializeField] private LayerMask groundMask;

    public Transform GroundCheck { get { return _groundCheck; } }
    public bool IsGrounded { get { return _isGrounded; } }
    public float Gravity { get { return _gravity; } set { _gravity = value; } }
    public float GroundedGravity { get { return _groundedGravity; } }
    public float GroundRad { get { return _groundRad; } }
    public LayerMask GroundMask { get { return groundMask; } }

    #endregion

    #region Jump Variables with getters and setters 

    [Header("Jumping Variables")]
    [SerializeField] private float _maxJumpVelocity = -50f;
    [SerializeField] private Vector3 _jumpVelocity;
    [SerializeField] private float _initialJumpVelocity;
    [SerializeField] private float _jumpGravity;
    [SerializeField] private bool _isPlayerJumping;

    public Vector3 JumpVelocity { get { return _jumpVelocity; } set { _jumpVelocity = value; } }
    public float JumpVelocity_Y { get { return _jumpVelocity.y; } set { _jumpVelocity.y = value; } }
    public float InitialJumpVelocity { get { return _initialJumpVelocity; } set { _initialJumpVelocity = value; } }
    public float JumpGravity { get { return _jumpGravity; } set { _jumpGravity = value; } }
    public bool IsPlayerJumping { get { return _isPlayerJumping; } set { _isPlayerJumping = value; } }

    #endregion

    #region impact

    [SerializeField] private bool _isAddingImpact = false;
    public bool IsAddingImpact { get { return _isAddingImpact; } set { _isAddingImpact = value; } }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        Rigidbody = GetComponentInParent<Rigidbody2D>();

        CheckGround();
    }
    
    public void LogicUpdate()
    {
        CheckGround();

        CurrentVelocity = Rigidbody.velocity;

        HandleSetGravity();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRad, groundMask);
    }


    #region gravity

    public void UpdateFallMultiplier()
    {

    }

    public void HandleSetGravity()
    {
        bool isFalling = JumpVelocity_Y <= 0.0f && _isPlayerJumping;
        float fallMultiplier = 2f;

        if (IsGrounded && !_isPlayerJumping)
        {
            JumpVelocity = Vector3.zero;
            JumpVelocity_Y = GroundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = JumpVelocity_Y;
            float newYVelocity = JumpVelocity_Y + (_jumpGravity * fallMultiplier * Time.deltaTime);
            float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20f);
            JumpVelocity_Y = nextYVelocity;
        }
        else
        {
            float previousYVelocity = JumpVelocity_Y;
            float newYVelocity = JumpVelocity_Y + (_jumpGravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            JumpVelocity_Y = nextYVelocity;
        }

        if (JumpVelocity_Y < _maxJumpVelocity)
        {
            JumpVelocity_Y = _maxJumpVelocity;
        }

        SetVelocityY(JumpVelocity_Y);
    }

    public void SetJumpVeloZero()
    {
        JumpVelocity = Vector3.zero;
    }

    #endregion

    public void HandleAddImpact(Vector3 forceDir, float addForce)
    {
        if (forceDir.y < 0)
        {
            forceDir.y = -forceDir.y;
        }

        SetVelocityZero();
        CanMove = false;
        IsPlayerJumping = true;
        _isAddingImpact = true;
        JumpVelocity = forceDir * addForce;
    }

    #region Set Functions

    public void SetVelocityZero()
    {
        _workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
        if (CanMove)
        {
            Rigidbody.velocity = _workspace;
            CurrentVelocity = _workspace;
        }
    }

    #endregion   

    #region Flip

    public void CheckIfShouldFlip()
    {
        if (_workspace.x > 0 && !_facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (_workspace.x < 0 && _facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        _facingRight = !_facingRight;

        //// Multiply the player's x local scale by -1.
        //Vector3 theScale = Rigidbody.transform.localScale;
        //theScale.x *= -1;
        //Rigidbody.transform.localScale = theScale;
        Rigidbody.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    #endregion

    #region Draw Functions
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        if (GroundCheck != null)
        {
            Gizmos.DrawSphere(GroundCheck.position, GroundRad);
        }
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : BaseState
{
    private bool _coyoteTime;
    private bool _wallJumpCoyoteTime;

    private bool _isTouchingWall = false;
    private bool _isTouchingLedge = false;

    private float _startWallJumpCoyoteTime;

    public PlayerInAirState(PlayerHandler player, StateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState() { }

    public override void UpdateState()
    {
        CheckCoyoteTime();
        CheckWallJumpCoyotime();
        CheckSwitchStates();
        // Debug.Log(_player.Core.Movement.JumpVelocity_Y);
    }


    public override void PhysicsUpdateState()
    {
        _isTouchingWall = _player.PlayerInteractor.CheckIfTouchingWall(_player.PlayerData.WallCheckDist);
        _isTouchingLedge = _player.PlayerInteractor.CheckIfTouchingLedge(_player.PlayerData.WallCheckDist,_player.Core.Movement.GroundMask); //ac

        if(_isTouchingWall && !_isTouchingLedge)
        {
            _player.LedgeClimbState.SetDetectedPosisiton(_player.transform.position);
        }

        if(!_wallJumpCoyoteTime && !_isTouchingWall)
        {
            StartWallJumpCoyoteTime();
        }
    }


    public override void ExitState() { }

    public override void InitializeSubState()
    {
        if (!_player.Core.Movement.IsMovementPressed)
        {
            SetSubState(_player.IdleState);
        }
        else if (_player.Core.Movement.IsMovementPressed)
        {
            SetSubState(_player.WalkState);
        }
    }

    public override void CheckSwitchStates()
    {
        CheckJumpMultiplier();

        if (_player.Core.Movement.IsGrounded && _player.Core.Movement.IsPlayerJumping && _player.Core.Movement.JumpVelocity_Y < 0.01f)
        {
            _player.AnimationController.animator.SetBool("isJumping", false);
            _player.Core.Movement.IsPlayerJumping = false;
            SwitchState(_player.GroundState);
            _player.PlayerEvents.OnLand();
            //SwitchState(_player.LandState);
        }
        else if (_player.InputHandler.PrimaryAttack)
        {
            SwitchState(_player.PrimaryAttackState);
        }
        // else if (_player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        // {
        //     // SwitchState(_player.JumpAttackState);
        // }
        //else if(_isTouchingWall && !_isTouchingLedge) //&& !_player.Core.Movement.IsGrounded)
        //{
        //    SwitchState(_player.LedgeClimbState);
        //}
        else if (_player.InputHandler.IsJumpPressed && _isTouchingWall)
        {
            SwitchState(_player.WallJumpState);
        }
        else if (_player.InputHandler.IsJumpPressed && _player.JumpState.CanJump() && !_player.Core.Movement.IsAddingImpact)//else if (_player.InputHandler.IsJumpPressed && (_isTouchingWall) || (_player.JumpState.CanJump() && !_player.Core.Movement.IsAddingImpact))
        {
            SwitchState(_player.JumpState);
        }
        else if (_isTouchingWall && _player.Core.Movement.JumpVelocity_Y < 0.15f && !_player.Core.Movement.IsGrounded)
        {
            SwitchState(_player.WallSlideState);
            _player.Core.Movement.IsPlayerJumping = false;
            _player.AnimationController.animator.SetBool("isJumping", false);
        }
        else if (_player.Core.Movement.IsGrounded && _player.Core.Movement.JumpVelocity_Y < 0.01f)
        {
            _player.AnimationController.animator.SetBool("isJumping", false);
            _player.Core.Movement.IsPlayerJumping = false;
            SwitchState(_player.GroundState);
            _player.PlayerEvents.OnLand(); //ses en ufak dusmede calisiyor duzeltmek lazim
            //SwitchState(_player.LandState);
        }
        else if (_player.InputHandler.IsDashPressed && _player.DashState.CanDash() && !_player.Core.Movement.IsAddingImpact) //else if (_player.InputHandler.IsDashPressed && _player.CanDash && !_player.IsDashing)
        {
            _player.AnimationController.animator.SetBool("isJumping", false);
            _player.Core.Movement.IsPlayerJumping = false;
            SwitchState(_player.DashState);
        }
    }

    private void CheckJumpMultiplier()
    {
        _player.AnimationController.UpdateAnimatorValues("yVelocity", _player.Core.Movement.JumpVelocity_Y);

        if (_player.Core.Movement.IsPlayerJumping)
        {
            if (_player.InputHandler.IsJumpReleased )
            {
                _player.Core.Movement.JumpVelocity_Y = _player.Core.Movement.CurrentVelocity.y * _player.Core.Movement.GroundedGravity;
                _player.Core.Movement.IsPlayerJumping = false;
            }
            else if (_player.Core.Movement.JumpVelocity_Y <= 0f)
            {
                _player.Core.Movement.IsPlayerJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (_coyoteTime && Time.time > _startTime + _player.PlayerData.CoyoteTime)
        {
            _coyoteTime = false;
            _player.JumpState.DecreaseJump();
        }
    }

    private void CheckWallJumpCoyotime()
    {
        if (_wallJumpCoyoteTime && Time.time > _startTime + _player.PlayerData.CoyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => _coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;
}

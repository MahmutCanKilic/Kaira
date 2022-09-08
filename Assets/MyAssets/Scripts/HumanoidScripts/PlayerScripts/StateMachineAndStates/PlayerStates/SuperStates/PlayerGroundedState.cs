using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : BaseState
{
    public PlayerGroundedState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState()
    {
        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.DashState.ResetAmountOfDashsLeft();
        _player.Core.Movement.SetVelocityZero();
        _player.Core.Movement.JumpVelocity_Y = _player.Core.Movement.GroundedGravity;
        _player.Core.Movement.CanMove = true;

        //_player.Core.Movement.IsAddingImpact = false;
    }

    public override void UpdateState()
    {
        CheckSwitchStates();

        //if (_player.InputHandler.f_Key_Press)
        //{
        //    _player.PlayerInteractor.HandlePressInteractable(true);
        //}
        //
        //if (_player.InputHandler.f_Key_Release)
        //{
        //    _player.PlayerInteractor.HandlePressInteractable(false);
        //}
    }

    public override void CheckSwitchStates()
    {

        if (_player.Core.Combat.IsPlayerDead)
        {
            SwitchState(_player.DyingState);
        }
        else if(_player.InputHandler.PrimaryAttack)
        {
            SwitchState(_player.PrimaryAttackState);
        }
        //else if(_player.InputHandler.SecondaryAttack)
        //{
        //   SwitchState(_player.SecondaryAttackState);
        //}
        else if (_player.Core.Movement.IsGrounded && _player.InputHandler.IsJumpPressed && _player.JumpState.CanJump() || _player.Core.Movement.IsAddingImpact)
        {
            SwitchState(_player.JumpState);
        }
        else if (!_player.Core.Movement.IsGrounded)// && !_isTouchingWall)
        {
            _player.InAirState.StartCoyoteTime();
            _player.AnimationController.PlayTargetAnimation("FallStateRouter", false);
            SwitchState(_player.InAirState);
        }
        else if (_player.InputHandler.IsDashPressed)// else if (_player.InputHandler.IsDashPressed && _player.CanDash && !_player.IsDashing)
        {
            SwitchState(_player.DashState);
        }
        // else if (_player.Core.Movement.IsGrounded && _isTouchingWall && _player.InputHandler.f_Key_Press)
        // {
        //     //_player.CharacterController.Move(new Vector3(0,0.25f,0) * Time.deltaTime);
        //     SwitchState(_player.WallClimbState);
        // }
    }

    public override void PhysicsUpdateState()
    {

    }

    public override void ExitState()
    {

    }

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




}

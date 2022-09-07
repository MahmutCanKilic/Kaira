using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public int _amountOfJumpsLeft;

    public PlayerJumpState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfJumpsLeft = _player.PlayerData.AmountOfJumps;
        InitializeSubState();
    }

    public override void EnterState()
    {
       // Debug.Log("jump");
        base.EnterState();
        _amountOfJumpsLeft--;
        HandleJump();
        _player.PlayerEvents.OnJump();
        _player.Core.Movement.SetVelocityY(_player.Core.Movement.JumpVelocity_Y);       
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _player.IsAbilityDone = true;

        CheckSwitchStates();
    }

    public override void PhysicsUpdateState()
    {
    }

    public override void ExitState()
    {
        base.ExitState();
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

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = _player.PlayerData.AmountOfJumps;

    public void DecreaseJump() => _amountOfJumpsLeft--;

    void HandleJump()
    {
        _player.Core.Movement.IsPlayerJumping = true;
        _player.AnimationController.PlayTargetAnimation("JumpRouter", false);       //Cancel rolling
        _player.AnimationController.animator.SetBool("isJumping", true);
        _player.Core.Movement.JumpVelocity_Y = _player.Core.Movement.InitialJumpVelocity * 0.5f;
    }

}

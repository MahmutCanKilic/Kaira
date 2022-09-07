using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public int _amountOfDashesLeft;

    public PlayerDashState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfDashesLeft = _player.PlayerData.AmountOfDashes;

    }

    public override void EnterState()
    {
        base.EnterState();

        _amountOfDashesLeft--;
        //damage yemeyi kapa**********************


        _player.JumpState.ResetAmountOfJumpsLeft();
        //_player.Core.Movement.CanMove = false;
        //_isPrimaryAttackPressed = false;
        //_isSecondaryAttackPressed = false;



        _player.AnimationController.PlayTargetAnimation("DodgeRollRouter", false);     //Play roll animation
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _currentSubState = null;
        _player.Core.Movement.SetVelocityX(_player.transform.right.x * _player.PlayerData.DashVelocity);
        // if (_player.InputHandler.AttackInputs[(int)CombatInputs.primary])
        // {
        //     _isPrimaryAttackPressed = true;
        //     _isSecondaryAttackPressed = false;
        // }
        //
        // if (_player.InputHandler.AttackInputs[(int)CombatInputs.secondary])
        // {
        //     _isSecondaryAttackPressed = true;
        //     _isPrimaryAttackPressed = false;
        // }

        CheckSwitchStates();
    }

    public override void PhysicsUpdateState() { }

    public override void ExitState()
    {
        base.ExitState();
        //damage yemeyi ac

        _player.Core.Movement.CanMove = true;

        _player.Core.Movement.SetVelocityX(_player.Core.Movement.WalkSpeed * _player.InputHandler.horizontalInput);


    }
    public override void InitializeSubState()
    {
    }

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();

        //dash attikta
        if (_player.IsAbilityDone && !_player.Core.Movement.IsGrounded)
        {
            _player.AnimationController.PlayTargetAnimation("FallStateRouter", false);
            SwitchState(_player.InAirState);
        }
        else if (!_player.IsAbilityDone && _player.InputHandler.IsJumpPressed)
        {
            SwitchState(_player.JumpState);
        }

        //dash sirasinda attack tusuna basilirsa guclu bir saldiri yapabilir
        //if (_player.IsAbilityDone && _player.Core.Movement.IsGrounded && _isPrimaryAttackPressed) //prim basinca second iptal etme kalkabilir ****
        //{
        //    SwitchState(_player.PrimaryAttackState);
        //    _isPrimaryAttackPressed = false;
        //    _isSecondaryAttackPressed = false;
        //}
        //else if (_player.IsAbilityDone && _player.Core.Movement.IsGrounded && _isSecondaryAttackPressed)
        //{
        //    SwitchState(_player.DashAttackState);
        //    _isPrimaryAttackPressed = false;
        //    _isSecondaryAttackPressed = false;
        //}
    }

    public bool CanDash()
    {
        if (_amountOfDashesLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void ResetAmountOfDashsLeft() => _amountOfDashesLeft = _player.PlayerData.AmountOfDashes;

    public void DecreaseJump() => _amountOfDashesLeft--;

    //animasyon eventi
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        _player.IsAbilityDone = true;
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        if (_player.Core.Movement.IsGrounded)
        {
            _player.PlayerEvents.OnDash();
        }
    }
}

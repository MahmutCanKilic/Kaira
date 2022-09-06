using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState

{    public PlayerPrimaryAttackState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        _player.AnimationController.PlayTargetAnimation("AttackRouter", false);

      
    }
    public override void UpdateState()
    {
        base.UpdateState();

        CurrentSubState = null;
        _player.Core.Movement.SetVelocityZero();

        //CalculateTargetAttackDiretion();

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();

        if(_player.IsAbilityDone && _player.Core.Movement.IsGrounded)
        {
            SwitchState(_player.GroundState);
        }
        else if(_player.InputHandler.IsJumpPressed)
        {
            SwitchState(_player.JumpState);
        }
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();        

        //damage check burada ya da interactorde olabilir //weaponda olmali gibi
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        //_player.PlayerEvents.OnAttack();
        //_player.WeaponController.CurrentWeapon.Action();

        Debug.Log("PrimAttackAction Triggered");
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        _player.IsAbilityDone = true;
    }

    public override void AnimationStartMovement()
    {
        base.AnimationStartMovement();
    }

    public override void AnimationStopMovement()
    {
        base.AnimationStopMovement();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerAbilityState
{
    private float _attackDirection;

    public PlayerPrimaryAttackState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimationController.PlayTargetAnimation("AttackRouter", false);

        _attackDirection = _player.InputHandler.verticalInput;

        _player.AnimationController.UpdateAnimatorValues("attackDirection",0f, _attackDirection);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();

        if(_player.IsAbilityDone && _player.Core.Movement.IsGrounded)
        {
            SwitchState(_player.GroundState);
        }
        else if(_player.IsAbilityDone && !_player.Core.Movement.IsGrounded)
        {
            _player.AnimationController.PlayTargetAnimation("FallStateRouter", false);
            SwitchState(_player.InAirState);
        }
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();        
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        _player.PlayerEvents.OnAttack();
        _player.PlayerInteractor.CheckDamage(_player.PlayerData.DamageAmount, _attackDirection);
       
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

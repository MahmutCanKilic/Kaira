using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : BaseState
{
    protected bool _isPrimaryAttackPressed = false;
    protected bool _isSecondaryAttackPressed = false;

    public PlayerAbilityState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
        _player.IsAbilityDone = false;
        _isPrimaryAttackPressed = false;
    }
    public override void EnterState()
    {
        _player.IsAbilityDone = false;
    }

    public override void UpdateState()
    {
        
    }

    public override void CheckSwitchStates()
    {
        if (_player.IsAbilityDone)
        {
            if (_player.Core.Movement.IsGrounded && _player.Core.Movement.JumpVelocity_Y < 0.01f)
            {
                SwitchState(_player.GroundState);
            }
            else
            {
                //_player.AnimationController.PlayTargetAnimation("FallStateRouter", false);
                SwitchState(_player.InAirState);
            }
        }
        else
        {
            if (_player.Core.Combat.IsPlayerDead)
            {
                SwitchState(_player.DyingState);
            }
            else if (_player.Core.Movement.IsAddingImpact)
            {
                SwitchState(_player.JumpState);
            }

        }
    }

    public override void PhysicsUpdateState()
    {
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }  

   
}

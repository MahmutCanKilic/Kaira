using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondaryAttackState : PlayerAbilityState
{  
    public PlayerSecondaryAttackState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationStartMovement()
    {
        base.AnimationStartMovement();
    }

    public override void AnimationStopMovement()
    {
        base.AnimationStopMovement();
    }

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void PhysicsUpdateState()
    {
        base.PhysicsUpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}

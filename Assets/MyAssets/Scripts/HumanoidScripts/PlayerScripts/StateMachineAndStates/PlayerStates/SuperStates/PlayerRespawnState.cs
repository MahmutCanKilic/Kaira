using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : BaseState
{
    public PlayerRespawnState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
    }

    public override void CheckSwitchStates()
    {
       
    }

    public override void EnterState()
    {
        _player.Core.Combat.Respawn();
        _player.AnimationController.PlayTargetAnimation("RespawningRouter", false);
        //set last checkpoint
    }

    public override void ExitState()
    {
       
    }

    public override void InitializeSubState()
    {
       
    }

    public override void PhysicsUpdateState()
    {
      
    }

    public override void UpdateState()
    {
        CurrentSubState = null;
       
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        SwitchState(_player.GroundState);
    }
}

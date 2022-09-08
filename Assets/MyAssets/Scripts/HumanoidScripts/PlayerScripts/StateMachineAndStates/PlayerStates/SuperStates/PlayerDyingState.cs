using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDyingState : BaseState
{
    public PlayerDyingState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        //dying animasyonuna giris
        _player.Core.Movement.CanSetGravity = false;
        _player.AnimationController.PlayTargetAnimation("DyingRouter", false);       
        //_player.PlayerEvents.OnDyingEvent();

        //game manager dying state gecis    
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
        _player.Core.Movement.SetVelocityZero();

    }

    public override void CheckSwitchStates()
    {
        if(!_player.Core.Combat.IsPlayerDead)
        {
            SwitchState(_player.RespawnState);
        }
    }

    public override void PhysicsUpdateState()
    {

    }

    public override void ExitState()
    {
        _player.Core.Movement.CanSetGravity = true;
    }

    public override void InitializeSubState()
    {
    }
}

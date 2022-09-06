using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    public PlayerWallJumpState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _player.AnimationController.PlayTargetAnimation("JumpRouter", false);       //Cancel rolling        

        _player.JumpState.ResetAmountOfJumpsLeft();
     
        //_player.Core.Movement.CheckIfShouldFlip();

        _player.JumpState.DecreaseJump();

        //_player.PlayerEvents.OnWallJump();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _currentSubState = null;

        //_player.Core.Movement.JumpVelocity_Y = _player.Core.Movement.InitialJumpVelocity * 0.5f;

        _player.Core.Movement.JumpVelocity_Y = _player.PlayerData.WallJumpVelocityY;
        _player.Core.Movement.SetVelocityX(-_player.transform.right.x * _player.PlayerData.WallJumpVelocityX);

        _player.AnimationController.UpdateAnimatorValues("yVelocity", _player.Core.Movement.CurrentVelocity.y);

        // _player.Core.Movement.SetVelocity(_player.PlayerData.WallJumpVelocity, _player.PlayerData.WallJumpAngle, (int)-_player.transform.right.x);


        _player.IsAbilityDone = true;
       //
       //if (_player.Core.Movement.JumpVelocity_Y<0)
       //{
       //    _player.IsAbilityDone = true;
       //}     

        CheckSwitchStates();

    }

    public override void CheckSwitchStates()
    {
        base.CheckSwitchStates();
    }
}

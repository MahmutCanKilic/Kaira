using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _currentSubState = null;

        //if (_player.Core.Movement.IsGrounded)
        //{
        //    _player.PlayerEvents.OnWallSlide();
        //}

        _player.AnimationController.PlayTargetAnimation("WallSlideStateRouter", false);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _player.Core.Movement.SetVelocityX(0);
        _player.Core.Movement.SetVelocityY(-_player.PlayerData.WallSlideVelocity);

        //if (grabInput && yInput == 0)
        //{
        //    stateMachine.ChangeState(player.WallGrabState);
        //}
    }
}

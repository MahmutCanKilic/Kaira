using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : BaseState
{
    protected bool _isTouchingWall;
    protected bool _isTouchingLedge;

    public PlayerTouchingWallState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_player.InputHandler.IsJumpPressed) //|| _player.Core.Movement.IsAddingImpact) //jumppad eklenirse
        {
            SwitchState(_player.WallJumpState);           
        }
        else if (_player.Core.Movement.IsGrounded)
        {
            SwitchState(_player.GroundState);            
        }
        else if (!_isTouchingWall)//&& _player.Core.Movement.JumpVelocity_Y < _player.Core.Movement.GroundedGravity)
        {           
            SwitchState(_player.InAirState);
        }
        else if (_isTouchingWall && !_isTouchingLedge)
        {
            //SwitchState(_player.LedgeClimbstate); //ac
        }
    }

    public override void PhysicsUpdateState()
    {
        _isTouchingWall = _player.PlayerInteractor.CheckIfTouchingWall(_player.PlayerData.WallCheckDist);
        // _isTouchingLedge = _player.PlayerInteractor.CheckIfTouchingLedgeHorizontal(); //ac

        // if (isTouchingWall && !isTouchingLedge)
        // {
        //     player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        // }


    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState() { }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        // _player.IsAbilityDone = true;
    }


}

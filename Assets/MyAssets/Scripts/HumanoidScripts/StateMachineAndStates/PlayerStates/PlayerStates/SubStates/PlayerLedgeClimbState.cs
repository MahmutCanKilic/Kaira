using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : BaseState
{
    private Vector2 _detectedPos;
    private Vector2 _cornerPos;
    private Vector2 _workspace;
    private Vector2 _startPos;
    private Vector2 _stopPos;

    public PlayerLedgeClimbState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    } 


    public override void EnterState()
    {
        _player.Core.Movement.SetVelocityZero();
        _player.transform.position = _detectedPos;
        _cornerPos = DetermineCornerPosition();

        _startPos.Set(_cornerPos.x - (_player.transform.right.x * _player.PlayerData.StartOffSet.x), _cornerPos.y - _player.PlayerData.StartOffSet.y);
        _stopPos.Set(_cornerPos.x - (_player.transform.right.x * _player.PlayerData.StopOffset.x), _cornerPos.y + _player.PlayerData.StartOffSet.y);

        _player.transform.position = _startPos;

        _player.AnimationController.PlayTargetAnimation("LedgeRouter", false);
    }


    public override void UpdateState()
    {
        _player.Core.Movement.SetVelocityZero();

        if(_isAnimationFinished)
        {
            SwitchState(_player.GroundState);
        }

        _player.transform.position = _startPos;
    }

    public override void CheckSwitchStates()
    {

    }

    public override void PhysicsUpdateState()
    {

    }

    public override void ExitState()
    {
        _player.transform.position = _stopPos;
    }

    public override void InitializeSubState()
    {
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public void SetDetectedPosisiton(Vector2 pos) => _detectedPos = pos;

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = _player.PlayerInteractor.RayHit(_player.PlayerInteractor.Hips.position, _player.transform.right, _player.Core.Movement.GroundMask, _player.PlayerData.WallCheckDist);
        float xDist = xHit.distance;
        _workspace.Set((xDist + 0.015f) * _player.transform.right.x, 0f);
        //RaycastHit2D yHit = _player.PlayerInteractor.RayHit(_player.PlayerInteractor.LedgeCheckHor.position + (Vector3)_player.Core.Movement.Workspace, Vector2.down, _player.Core.Movement.GroundMask, _player.PlayerInteractor.LedgeCheckHor.position.y - _player.PlayerInteractor.Hips.position.y + 0.015f);
        RaycastHit2D yHit = _player.PlayerInteractor.RayHit(_player.PlayerInteractor.LedgeCheckHor.position, Vector3.down, _player.Core.Movement.GroundMask, (_player.PlayerInteractor.LedgeCheckHor.position.y - _player.PlayerInteractor.Hips.position.y));

        float yDist = yHit.distance;

        _workspace.Set(_player.PlayerInteractor.Hips.position.x + (xDist * _player.transform.right.x), _player.PlayerInteractor.LedgeCheckHor.position.y - yDist);

        return _workspace;
        // float yDist = yHit.distance;
    }

}

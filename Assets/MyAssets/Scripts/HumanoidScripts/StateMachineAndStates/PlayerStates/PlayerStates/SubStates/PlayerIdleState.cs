using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void EnterState()
    {
        _player.Core.Movement.SetVelocityX(0f);
    }

    public override void UpdateState()
    {
        _player.AnimationController.UpdateAnimatorValues("moveAmount", 0, 0); //simdilik burada kalsinlar ama surekli guncelliyorlar ona dikkat et
        //_player.AnimationController.UpdateAnimatorValues("Vertical", 0);
        //_player.AnimationController.UpdateAnimatorValues("Horizontal", 0);

        CheckSwitchStates();
    }

    public override void PhysicsUpdateState() { }


    public override void ExitState()
    {

    }

    public override void CheckSwitchStates()
    {
        if (_player.Core.Movement.IsMovementPressed)
        {
            SwitchState(_player.WalkState);
        }
    }

    public override void InitializeSubState() { }
}

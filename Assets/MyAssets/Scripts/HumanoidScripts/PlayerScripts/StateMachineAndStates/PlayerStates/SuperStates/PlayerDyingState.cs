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
        //game manager dying state gecis
        //event dying event
        //hasar almayi ve dusmanlarin gormesini kapa (?)

    }

    public override void UpdateState()
    {

    }

    public override void CheckSwitchStates()
    {

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

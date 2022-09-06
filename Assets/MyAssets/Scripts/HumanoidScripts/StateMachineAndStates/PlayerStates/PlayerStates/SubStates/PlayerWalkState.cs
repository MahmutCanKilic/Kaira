using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : BaseState
{
   // private bool _facingRight = true;

    public PlayerWalkState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName) { }

    public override void EnterState()
    {

    }
    public override void UpdateState()
    {
        HandleMovement();
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (!_player.Core.Movement.IsMovementPressed)
        {
            SwitchState(_player.IdleState);
        }
    }

    private void HandleMovement()
    {
        if (_player.Core.Movement.CanMove && !_player.Core.Movement.IsAddingImpact)
        {
            if (_player.Core.Movement.IsGrounded)
            {
                _player.PlayerEvents.OnMove();
            }

            // _player.Core.Movement.UpdateMoveDirectionNormalized(_player.Core.Movement.VerVector * _player.InputHandler.verticalInput + _player.Core.Movement.HorVector * _player.InputHandler.horizontalInput);
            //_player.Core.Movement.UpdateMoveDirectionNormalized(_player.InputHandler.movementInput);

            _player.Core.Movement.SetVelocityX(_player.Core.Movement.WalkSpeed * _player.InputHandler.horizontalInput);

            _player.Core.Movement.CurrentSpeed = _player.Core.Movement.WalkSpeed;   //Set current speed to walk speed   

            #region flip character

           //if (_player.Core.Movement.MoveDirection.x > 0 && !_facingRight)
           //{
           //    // ... flip the player.
           //    Flip();
           //}
           //// Otherwise if the input is moving the player left and the player is facing right...
           //else if (_player.Core.Movement.MoveDirection.x < 0 && _facingRight)
           //{
           //    // ... flip the player.
           //    Flip();
           //}
           //
            #endregion          

            //Update animator Variables
            _player.AnimationController.UpdateAnimatorValues("moveAmount", _player.InputHandler.moveAmount);
            _player.AnimationController.UpdateAnimatorValues("Vertical", _player.InputHandler.verticalInput);
            _player.AnimationController.UpdateAnimatorValues("Horizontal", _player.InputHandler.horizontalInput);

            _player.Core.Movement.CheckIfShouldFlip();
        }
    }

   //private void Flip()
   //{
   //    // Switch the way the player is labelled as facing.
   //    _facingRight = !_facingRight;
   //
   //    // Multiply the player's x local scale by -1.
   //    Vector3 theScale = _player.transform.localScale;
   //    theScale.x *= -1;
   //    _player.transform.localScale = theScale;
   //}

    public override void PhysicsUpdateState() { }
    public override void ExitState() { }
    public override void InitializeSubState() { }
}

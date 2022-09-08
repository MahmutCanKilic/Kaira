using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    private bool _isRootState = false;

    protected StateMachine _playerSM; //sonra humanoid olabilir
    protected BaseHumanoid _humanoid;
    protected PlayerHandler _player;
    //protected Enemy1Controller _enemy;
    protected PlayerData _playerData;

    protected BaseState _currentSubState;
    protected List<BaseState> _currentSuperState; //gerek var mi ?

    protected bool _isAnimationFinished; //kullanýlmýyor gibi silinebilir
    protected float _startTime;
    private string _animBoolName;

    public bool IsRootState { get { return _isRootState; } set { _isRootState = value; } }
    public List<BaseState> CurrentSuperState { get { return _currentSuperState; } set { _currentSuperState = value; } }
    public BaseState CurrentSubState { get { return _currentSubState; } set { _currentSubState = value; } }

    public BaseState(PlayerHandler player, StateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this._humanoid = player;
        this._player = player;
        this._playerSM = stateMachine;
        this._animBoolName = animBoolName;
        _currentSuperState = new List<BaseState>();
    }

    //public BaseState(Enemy1Controller enemy, StateMachine stateMachine, EnemyData enemyData, string animBoolName)
    //{
    //    this._humanoid = enemy;
    //    this._enemy = enemy;
    //    this._humanoidSM = stateMachine;
    //    this._animBoolName = animBoolName;
    //    _currentSuperState = new List<BaseState>();
    //}

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void PhysicsUpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();


    public void EnterStates()
    {
        EnterState();
        _humanoid.AnimationController.animator.SetBool(_animBoolName, true);
        _startTime = Time.time;
        _isAnimationFinished = false;
        Debug.Log(_animBoolName);
    }

    public void UpdateStates()
    {
        UpdateState();

        if (_currentSubState != null)
        {
            _currentSubState.UpdateStates();
        }   
    }

    public void PhysicsUpdateStates()
    {
        PhysicsUpdateState();

        if (_currentSubState != null)
        {
            _currentSubState.PhysicsUpdateStates();
        }
    }

    public void ExitStates()
    {
        ExitState();
        _player.AnimationController.animator.SetBool(_animBoolName, false);
    }

    public void SwitchState(BaseState newState)
    {
        //current state exits state
        ExitStates();

        //new state enters state
        newState.EnterStates();

        //switch current state of context
        if (_isRootState) 
        {
            _humanoid.stateMachine.CurrentState = newState;
        }
        else if (_currentSuperState != null)
        {
            _humanoid.stateMachine.CurrentState.SetSubState(newState);
        }
    }

    public void SetSuperState(BaseState newSuperState)
    {
        _currentSuperState.Add(newSuperState);
    }

    public void SetSubState(BaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => _isAnimationFinished = true;
    public virtual void AnimationStartMovement() { }
    public virtual void AnimationStopMovement() { }
    public virtual void AnimationActionTrigger() { }
}
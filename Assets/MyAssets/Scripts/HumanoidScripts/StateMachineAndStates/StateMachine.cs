using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState _currentState;
    public BaseState CurrentState { get { return _currentState; } set { _currentState = value; } }

    public void Initialize(BaseState startingState)
    {
        _currentState = startingState;
        _currentState.EnterStates();
    }
}
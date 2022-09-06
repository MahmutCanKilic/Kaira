using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BaseHumanoid : MonoBehaviour
{
    #region currentStateName

    [Header("Current State")]
    public string currentStateName;
    public string currentSubStateName;

    #endregion

    #region Components 

    public Core Core { get; private set; }
    //public BasicFunctions BasicFunctions { get; private set; }
    public StateMachine stateMachine { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public PlayerInteractor PlayerInteractor { get; private set; } //***** düzelt     

    #endregion

    public virtual void Awake()
    {
        #region components

        Core = GetComponentInChildren<Core>();
        AnimationController = GetComponentInChildren<AnimationController>();

        PlayerInteractor = GetComponentInChildren<PlayerInteractor>(); //enemyde yok ona ekle

        #endregion

        stateMachine = new StateMachine();
        //BasicFunctions = new BasicFunctions();//ac
    }

    public virtual void Start() { }

    public virtual void Update()
    {
        stateMachine.CurrentState.UpdateStates();
        Core.LogicUpdate();

        #region CurrentState / CurrentSubState Names

        if (stateMachine.CurrentState != null)
        {
            currentStateName = stateMachine.CurrentState.ToString();

            if (stateMachine.CurrentState.CurrentSubState != null)
            {
                currentSubStateName = stateMachine.CurrentState.CurrentSubState.ToString();
            }
            else
            {
                currentSubStateName = "null";
            }
        }
        else
        {
            currentStateName = "null";
        }

        #endregion
    }

    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdateStates();//ac
    }


    public virtual void HandleAddImpact(Vector3 forceDir, float addForce)
    {
        Core.Movement.HandleAddImpact(forceDir, addForce);
    }

    #region Check Functions 

    public void CheckTimer(float time, Action<bool> callback)
    {
       //StartCoroutine(BasicFunctions.BasicTimer(time, result =>
       //{
       //    if (result)
       //    {
       //        callback(result);
       //    }
       //}
       //));
    }

    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    BaseHumanoid _humanoid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _humanoid = GetComponentInParent<BaseHumanoid>();
    }

    public void UpdateAnimatorValues(string getString, float getValue)
    {
        animator.SetFloat(getString, getValue, 0.15f, Time.deltaTime);
    }

    public void UpdateAnimatorValues(string getString,float dampTime, float getValue)
    {
        animator.SetFloat(getString, getValue, dampTime, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnim, bool isRootMotion)
    {
        animator.CrossFade(targetAnim, 0.15f);
    }
    private void AnimationTrigger() => _humanoid.stateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => _humanoid.stateMachine.CurrentState.AnimationFinishTrigger();
    private void AnimationStartMovement() => _humanoid.stateMachine.CurrentState.AnimationStartMovement();
    private void AnimationStopMovement() => _humanoid.stateMachine.CurrentState.AnimationStopMovement();
    private void AnimationActionTrigger() => _humanoid.stateMachine.CurrentState.AnimationActionTrigger();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public abstract class State : MonoBehaviour
{
    protected abstract Animator anim { get; set; }

    protected abstract StateMachine stm { get; set; }

    protected abstract Coroutine coroutineHolder { get; set; }

    public abstract int Priority { get; set; }

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        stm = GetComponent<StateMachine>();
    }

    public virtual void OnCall()
    {
        coroutineHolder = StartCoroutine(StateCoroutine());
        PlayAnimation();
    }
    public virtual void Interrupt()
    {
        if (coroutineHolder == null) return;
        StopCoroutine(coroutineHolder);
    }
    protected virtual void OnFinish()
    {
        stm.ActionComplete();
    }
    protected abstract void PlayAnimation();

    protected abstract IEnumerator StateCoroutine();
}

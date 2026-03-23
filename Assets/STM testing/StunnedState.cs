using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : State
{
    protected override Animator anim { get; set; }
    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }
    public override int Priority { get; set; }

    [SerializeField] int priority;

    [SerializeField] float stunTime;

    Rigidbody2D rb;

    public AK.Wwise.Event stunsound;
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        Priority = priority;
    }

    public override void OnCall()
    {
        base.OnCall();

        rb.velocity = Vector2.zero;
    }

    protected override void OnFinish()
    {
        base.OnFinish();
        Debug.Log("Stunned Finished");
    }

    protected override void PlayAnimation()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundController>().PlayBlockSuccess();
        anim.Play("Stunned");
    }

    protected override IEnumerator StateCoroutine()
    {
        stunsound.Post(gameObject);
        yield return new WaitForSeconds(stunTime);
        OnFinish();
    }
}

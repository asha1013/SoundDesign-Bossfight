using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashState : State
{
    protected override Animator anim { get ; set; }
    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }
    public override int Priority { get; set; }

    [SerializeField] int priority;

    [SerializeField] float duration;
    [SerializeField] float endTime;
    

    Rigidbody2D rb;

    public AK.Wwise.Event SwordSound;
    public AK.Wwise.Event hitGround;
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
        Debug.Log("Slash Finished");
    }

    protected override void PlayAnimation()
    {
        PlaySwordSound();
        anim.Play("Slash");
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(endTime);
        OnFinish();
    }

    public void PlaySwordSound()
    {
        SwordSound.Post(gameObject);
        
    }

    public void PlayGroundHit()
    {
        hitGround.Post(gameObject);
    }
}

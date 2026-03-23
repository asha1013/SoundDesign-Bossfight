using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockableSlash : State
{
    public override int Priority { get; set; }
    protected override Animator anim { get; set; }
    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }

    [SerializeField] int priority;

    [SerializeField] float duration;
    [SerializeField] float endTime;
    //[SerializeField] GameObject hitbox;
    [SerializeField] bossHitScript hitboxScript;
    [SerializeField] int damage;

    Rigidbody2D rb;

    public AK.Wwise.Event SwordSound;
    public AK.Wwise.Event breathSound;
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

    protected override void PlayAnimation()
    {
        PlaySwordSound();
        anim.Play("UnblockableSlash");
    }

    protected override IEnumerator StateCoroutine()
    {
        hitboxScript.unblockable = true;
        hitboxScript.damage = damage;
        yield return new WaitForSeconds(duration);
        hitboxScript.unblockable = false;
        hitboxScript.damage = 15;
        yield return new WaitForSeconds(endTime);
        OnFinish();
    }

    public void PlaySwordSound()
    {
        SwordSound.Post(gameObject);
        
    }
    public void BreathSound()
    {
        breathSound.Post(gameObject);
    }
    public void PlayGroundHitSound()
    {
        hitGround.Post(gameObject);
    }
}
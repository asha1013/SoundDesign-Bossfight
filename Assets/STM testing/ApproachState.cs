using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachState : State
{
    protected override Animator anim { get; set; }

    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }
    public override int Priority { get; set; }

    [SerializeField] int priority;

    [SerializeField] GameObject player;
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;

    public AK.Wwise.Event FootstepSound;

    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        Priority = priority;
    }

    public override void OnCall()
    {
        base.OnCall();
        //Debug.Log("Approach Called");
    }

    protected override void OnFinish()
    {
        base.OnFinish();
        //Debug.Log("Approach Finished");
    }

    protected override void PlayAnimation()
    {
        anim.Play("Approach");
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return null;
    }

    private void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }
    }

    public void PlayFootstepSound()
    {
        FootstepSound.Post(gameObject);
    }

}

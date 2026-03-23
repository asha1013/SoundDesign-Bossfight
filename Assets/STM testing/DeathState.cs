using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathState : State
{
    public override int Priority { get; set; }
    protected override Animator anim { get; set; }
    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }

    [SerializeField] int priority;

    Rigidbody2D rb;
    Collider2D bossCollider;

    public AK.Wwise.Event deathSound;
    protected override void Start()
    {
        base.Start();
         
        rb = GetComponent<Rigidbody2D>();
        bossCollider = GetComponent<Collider2D>();
        Priority = priority;
    }

    public override void OnCall()
    {
        base.OnCall();
        deathSound.Post(gameObject);
        rb.velocity = Vector2.zero;
        bossCollider.enabled = false;
    }

    protected override void PlayAnimation()
    {
        anim.Play("Death");
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("WinScene");
    }
}

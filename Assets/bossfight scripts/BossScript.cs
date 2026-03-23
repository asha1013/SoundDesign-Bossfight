using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject flameWave;
    [SerializeField] LayerMask mask;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D col;

    enum State
    {
        Stunned,
        Approach,
        FlameSlash,
        FlameWave,
        Dead,
    };

    State state = new();

    [SerializeField] float moveSpeed;

    [SerializeField] float stunDuration;

    [SerializeField] float flameSlashTriggerDistance;

    [SerializeField] float flameWaveCooldown;

    [SerializeField] float flameSlashCastTime;
    [SerializeField] float flameWaveCastTime;

    [SerializeField] int maxHealth;
    public int currentHealth;

    Coroutine currentSlash;
    [SerializeField] float flameWaveChance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = State.Approach;
        currentHealth = maxHealth;
        col = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            rb.velocity = Vector2.zero;
            state = State.Dead;
            anim.Play("death");
        }
        switch (state)
        {
            case State.Approach:
                anim.Play("walk");

                if (Physics2D.OverlapCircle(transform.position, flameSlashTriggerDistance, mask))
                {
                    state = State.FlameSlash;
                    currentSlash = StartCoroutine(FlameSlashCastCoroutine());
                } else
                {
                    /*if(Random.Range(0, 1000) < flameWaveChance)
                    {
                        state = State.FlameWave;
                        StartCoroutine(FlameWaveCastCoroutine());
                    }*/
                }
                break;

            case State.Stunned:
                anim.Play("takeHit");
                break;

            case State.FlameSlash:
                anim.Play("slash");
                break;

            case State.FlameWave:
                anim.Play("slash");
                break;

            case State.Dead:
                col.enabled = false;
                break;

        }

        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Approach:
                Approach();
                break;

            case State.Stunned:
                rb.velocity = Vector2.zero;
                break;

            case State.FlameSlash:
                rb.velocity = Vector2.zero;
                break;

            case State.FlameWave:
                rb.velocity = Vector2.zero;
                break;

        }
    }

    public void Stunned()
    {
        state = State.Stunned;
        if(currentSlash != null) StopCoroutine(currentSlash);
        StartCoroutine(StunnedCoroutine());
    }

    void Approach()
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

    void FlameWave()
    {
        Instantiate(flameWave, new Vector3(transform.position.x, -3.7f, transform.position.z), transform.rotation);
    }

    IEnumerator StunnedCoroutine()
    {
        yield return new WaitForSeconds(stunDuration);
        //Debug.Log("stun finished");
        state = State.Approach;
    }

    IEnumerator FlameWaveCastCoroutine()
    {
        yield return new WaitForSeconds(flameWaveCastTime);
        FlameWave();
        state = State.Approach;
    }

    IEnumerator FlameSlashCastCoroutine()
    {
        yield return new WaitForSeconds(flameSlashCastTime);
        //Debug.Log("slash finished");
        state = State.Approach;
    }

    public void Damage(int data)
    {
        currentHealth -= data;
    }
    
}

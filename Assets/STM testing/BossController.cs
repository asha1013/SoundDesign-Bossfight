using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] float flameSlashTriggerDistance;
    [SerializeField] LayerMask mask;

    [SerializeField] int maxHealth;
    [SerializeField] GameObject player;
    public int currentHealth;

    public int slashCounter;
    public int parryCounter;

    [SerializeField] float projectileTimer;
    //SpriteRenderer sr;

    StateMachine stm;

    private void Start()
    {
        stm = GetComponent<StateMachine>();
        //sr = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        StartCoroutine(ProjectileCoroutine());
    }

    private void Update()
    {
        if(stm.activeState == stm.defaultState)
        {
            if (Physics2D.OverlapCircle(transform.position, flameSlashTriggerDistance, mask))
            {
                stm.RequestState(stm.slashState);
            }
        }

        if (currentHealth <= 0)
        {
            stm.RequestState(stm.deathState);
        }

        if(slashCounter >= 3)
        {
            slashCounter = 0;
            parryCounter = 0;
            stm.RequestState(stm.unblockableState);
        }
        if(parryCounter >= 3)
        {
            parryCounter = 0;
            slashCounter = 0;
            stm.RequestState(stm.unblockableState);
        }
        
    }

    public void Damage(int data)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoundController>().PlayHit();
        currentHealth -= data;
    }

    IEnumerator ProjectileCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectileTimer);
            stm.RequestState(stm.flameWaveState);
        }
    }
}

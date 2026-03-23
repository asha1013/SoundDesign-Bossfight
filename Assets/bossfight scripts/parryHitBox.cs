using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parryHitBox : MonoBehaviour
{
    [SerializeField] GameObject boss;
    StateMachine bossStateMachine;

    private void Start()
    {
        bossStateMachine = boss.GetComponent<StateMachine>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile")) return;
        bossStateMachine.RequestState(bossStateMachine.stunnedState);
        cameraShake.Instance.Shake(2, 0.5f);
    }
}

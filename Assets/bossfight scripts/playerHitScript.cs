using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitScript : MonoBehaviour
{
    [SerializeField] GameObject boss;
    BossController bossController;

    [SerializeField] int damage;

    private void Start()
    {
        bossController = boss.GetComponent<BossController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossController.Damage(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHitScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerMovementBossfight pm;

    public int damage;

    public bool unblockable;

    private void Start()
    {
        pm = player.GetComponent<PlayerMovementBossfight>();

    }

    private void Update()
    {
        cameraShake.Instance.Shake(2, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (unblockable == false)
        {
            if (pm.isParrying == false)
            {
                pm.Damage(damage);
            }
        }
        else
        {
            pm.Damage(damage);
        }
    }
}

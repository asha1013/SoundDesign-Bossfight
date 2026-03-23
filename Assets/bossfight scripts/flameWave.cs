using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameWave : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (transform.localScale.x > 0) ? new Vector2(-speed, 0) : new Vector2(speed, 0);
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovementBossfight pm = collision.gameObject.GetComponent<PlayerMovementBossfight>();
            if (pm.isParrying == false)
            {
                pm.Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}

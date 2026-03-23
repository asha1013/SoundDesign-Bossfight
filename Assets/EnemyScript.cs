using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    float hidingRange;

    [SerializeField]
    LayerMask mask;

    [SerializeField]
    float speed;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, hidingRange, mask);
        if (collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }
}

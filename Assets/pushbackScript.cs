using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pushbackScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int damage;
    [SerializeField] float interval;
    bool ready;
    // Start is called before the first frame update
    void Start()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!ready) return;
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerMovementBossfight>().rolling == false)
        {
            player.GetComponent<PlayerMovementBossfight>().Damage(0);
            StartCoroutine(DamageTick());
            ready = false;
        }
    }

    IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(interval);
        ready = true;
    }
}

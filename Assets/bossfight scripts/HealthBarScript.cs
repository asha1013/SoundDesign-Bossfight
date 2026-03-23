using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    PlayerMovementBossfight pm;

    Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        pm = player.GetComponent<PlayerMovementBossfight>();
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = pm.currentHealth / 100f;

        if (pm.currentHealth <= 0)
        {
            // 例如直接加载一个名为 "DeathScene" 的场景
            SceneManager.LoadScene("DeathScence");

        }
    }
}

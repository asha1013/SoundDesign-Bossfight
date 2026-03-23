using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealthBarScript : MonoBehaviour
{
    [SerializeField] GameObject player;

    BossController bossController;

    Slider healthBar;
    private bool isBossDefeated = false;


    void Start()
    {
        bossController = player.GetComponent<BossController>();
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = bossController.currentHealth / 100f;
        if (!isBossDefeated && bossController.currentHealth <= 0)
        {
            isBossDefeated = true;
            // Boss 死亡后切换到胜利场景（WinScene）
            //SceneManager.LoadScene("WinScene");
        }
    }
}

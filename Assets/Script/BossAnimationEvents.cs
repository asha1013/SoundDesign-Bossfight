using UnityEngine;

public class BossAnimationEvents : MonoBehaviour
{
    [Tooltip("Player物体，用于访问播放音效的脚本")]
    public GameObject player;

    // Play the successful block sound effect (triggered by the first frame of the Stunned animation)
    public void NotifyBlockSuccess()
    {
        if (player != null)
        {
            PlayerSoundController soundController = player.GetComponent<PlayerSoundController>();
            if (soundController != null)
            {
                soundController.PlayBlockSuccess();
            }
            else
            {
                Debug.LogWarning("PlayerSoundController 未挂载在 Player 上。");
            }
        }
        else
        {
            Debug.LogWarning("未在 BossAnimationEvents 中设置 Player 引用。");
        }
    }
}

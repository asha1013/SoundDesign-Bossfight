using UnityEngine;
using UnityEngine.UI;

public class HealthBarWatcher : MonoBehaviour
{
    public Slider healthSlider; // Drag in the UI health bar Slider
    public PlayerSoundController soundController; // Drag in the sound control script
    private bool hasPlayedDeathSound = false;

    void Update()
    {
        if (healthSlider.value <= 0f && !hasPlayedDeathSound)
        {
            soundController.PlayDeath();
            hasPlayedDeathSound = true;
        }
    }
}

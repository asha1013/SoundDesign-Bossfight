using UnityEngine;

public class ShieldBlockDetector : MonoBehaviour
{
    private PlayerSoundController soundController;

    private void Start()
    {
        // Find the PlayerSoundController from the parent object
        soundController = GetComponentInParent<PlayerSoundController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it is flameWave(Clone)
        if (other.gameObject.name.Contains("flameWave"))
        {
            if (soundController != null)
            {
                soundController.PlayBlockSuccess();
            }
        }
    }
}

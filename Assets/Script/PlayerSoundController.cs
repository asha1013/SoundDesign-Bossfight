using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    private bool wasInAir = false;
    public bool isBlocking = false;

    // Footsteps
    public void PlayFootstep()
    {
        AkSoundEngine.PostEvent("Play_Footstep", gameObject);
    }

    // Swing the Sword
    public void PlaySwordWhoosh()
    {
        AkSoundEngine.PostEvent("Play_SwordWhoosh", gameObject);
    }

    // Hit the enemy
    public void PlayImpact()
    {
        AkSoundEngine.PostEvent("Play_Impact", gameObject);
    }

    // Block Start
    public void PlayBlockStart()
    {
        isBlocking = true;
        AkSoundEngine.PostEvent("Play_BlockStart", gameObject);
    }

    // Block End
    public void StopBlock()
    {
        isBlocking = false;
    }

    // Block Success
    public void PlayBlockSuccess()
    {
        AkSoundEngine.PostEvent("Play_BlockSuccess", gameObject);
    }

    // Get Hit
    public void PlayHit()
    {
        AkSoundEngine.PostEvent("Play_Hit", gameObject);
    }

    // Death
    public void PlayDeath()
    {
        AkSoundEngine.PostEvent("Play_Death", gameObject);
    }

    // Roll
    public void PlayRoll()
    {
        AkSoundEngine.PostEvent("Play_BetterRoll", gameObject);
    }

    // Jump
    public void PlayJump()
    {
        AkSoundEngine.PostEvent("Play_Jump", gameObject);
    }

    // Landing (triggered by collision)
    public void PlayJumpLand()
    {
        AkSoundEngine.PostEvent("Play_JumpLand", gameObject);
    }

    // Collision detection implementation logic
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Square"))
        {
            if (wasInAir)
            {
                PlayJumpLand();
                wasInAir = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Square"))
        {
            wasInAir = true;
        }
    }
}

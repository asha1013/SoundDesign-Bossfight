using UnityEngine;

public class AttackImpactDetector : MonoBehaviour
{
    public PlayerSoundController soundController;

    // Attack detection position (defaults to current character position if empty)
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask bossLayer;

    public void CheckAttackImpact()
    {
        // If the position is empty, use the character's current transform
        Vector2 center = attackPoint != null ? attackPoint.position : transform.position;

        // Check if there is a boss in attack range
        Collider2D hit = Physics2D.OverlapCircle(center, attackRange, bossLayer);
        if (hit != null)
        {
            soundController.PlayImpact();
        }
    }

    // View the attack range in the Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 center = attackPoint != null ? attackPoint.position : transform.position;
        Gizmos.DrawWireSphere(center, attackRange);
    }
}

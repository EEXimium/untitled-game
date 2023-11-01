using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            return; // Don't take damage if already dead
        }

        currentHealth -= damage;
        Debug.Log("NPC takes damage. Current Health: " + currentHealth);

        animator.SetTrigger("IsHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyKnockback(Vector2 KnockbackDirec, int knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;  // Reset any existing velocity       
        rb.AddForce(KnockbackDirec * knockbackForce, ForceMode2D.Impulse);
    }

    private void Die()
    {
        Debug.Log("NPC dies.");
        animator.SetBool("IsDead", true);

        // Disable NPC's collider or any other interactions
        Collider2D npcCollider = GetComponent<Collider2D>();
        if (npcCollider != null)
        {
            npcCollider.enabled = false;
        }

        // Insert any other death-related logic here

        // Optionally, destroy the GameObject after a delay
        float destroyDelay = 2.0f; // Adjust this delay as needed
        Destroy(gameObject, destroyDelay);
    }
}

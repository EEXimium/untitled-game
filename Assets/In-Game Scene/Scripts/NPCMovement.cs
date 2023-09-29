using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform target; 
    public float moveSpeed = 3f; 
    public float detectionRange = 5f; 
    public float attackRange = 1.5f; 
    public float attackCooldown = 1f; 
    public float hitStopDuration = 1f; // Duration to stop when hit
    public bool isDead = false; // Flag to indicate if NPC is dead

    private Rigidbody2D rb;
    private Animator animator;
    private float lastAttackTime;
    private bool isHit = false; // Flag to indicate if NPC is hit

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {       
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (!isHit && distanceToPlayer <= detectionRange)
        {
            // Calculate the direction to the player
            Vector2 directionToPlayer = (target.position - transform.position).normalized;

            // Calculate horizontal and vertical values based on the direction to the player
            float horizontalInput = directionToPlayer.x;
            float verticalInput = directionToPlayer.y;

            // Move the NPC towards the player using the calculated direction
            rb.velocity = directionToPlayer * moveSpeed;

            // Update walking animation based on directionToPlayer
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", verticalInput);

            // Check attack conditions and perform attacks if necessary
            if (!animator.GetBool("IsAttacking") && distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {                
                Attack();
            }
        }
        else
        {
            // Stop moving if the player is not in range
            rb.velocity = Vector2.zero;
        }
    }

    public void Hit()
    {
        // Trigger hit animation and stop moving for hitStopDuration seconds
        isHit = true;
        animator.SetTrigger("Hit");
        rb.velocity = Vector2.zero;
        Invoke("EndHit", hitStopDuration);
    }

    private void EndHit()
    {
        isHit = false;
    }

    private void Attack()
    {
        // NPC attacks the player
        Debug.Log("NPC attacks the player.");
        animator.SetBool("IsAttacking", true);
        
        // Deal damage to the player
        
        //  PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        //  if (playerHealth != null)
        //  {
        //      playerHealth.TakeDamage(attackDamage);
        //  }
        
        lastAttackTime = Time.time;
    }
}
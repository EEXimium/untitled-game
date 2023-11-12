using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;

    public float moveSpeed = 3f; 
    public float detectionRange = 5f; 
       
    public bool notMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {       
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange)
        {
            notMoving= false;
            // Calculate the direction to the player
            Vector2 directionToPlayer = (target.position - transform.position).normalized;

            // Move the NPC towards the player using the calculated direction
            rb.velocity = directionToPlayer * moveSpeed;
        }
        else
        {
            // Stop moving if the player is not in range
            rb.velocity = Vector2.zero;
            notMoving = true;
        }
    }

}
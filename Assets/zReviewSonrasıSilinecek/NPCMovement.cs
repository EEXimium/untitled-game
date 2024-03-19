using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody2D rb;
    //private Animator anim;

    public float moveSpeed = 3f; 
    public float detectionRange = 5f;
    public float shootRange = 8f;
    public float distanceToPlayer;

    public bool moving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {       
        distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange)
        {
            moving = true;
            // Calculate the direction to the player
            Vector2 directionToPlayer = (target.position - transform.position).normalized;

            if(this.tag == "ExplosiveNPC" || this.tag == "SplitMOB")
            {
                // Move the NPC towards the player using the calculated direction
                rb.velocity = directionToPlayer * moveSpeed;
            }else if(this.tag == "RangedNPC")
            {
                rb.velocity = -directionToPlayer * moveSpeed;
            }

        }
        else
        {
            // Stop moving if the player is not in range
            moving = false;
            rb.velocity = Vector2.zero;
            
        }
    }

}
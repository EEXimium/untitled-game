using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    protected Transform target;
    protected Rigidbody2D rb;
    protected Vector2 directionToPlayer;
    protected Vector2 goDirection;

    protected Transform DropPoint;

    protected Animator animator;
    protected SpriteRenderer spriterenderer;

    [Header("Movement Settings")] //INSPECTOR'DAN AYARLAYIN! KODDA DEÐER GÝRMEYÝN!

    public float moveSpeed;
    public float detectionRange;
    public float distanceToPlayer;
    public bool moving;

    [Header("Health Settings")] //INSPECTOR'DAN AYARLAYIN! KODDA DEÐER GÝRMEYÝN!
    public float maxHealth;
    public float currentHealth;
    public bool isDeath = false;

    [Header("Other Settings")]
    public GameObject deathCoin;

    //--------------ANIMATION
    protected private string currentState;

    protected const string Idle = "Idle";
    protected const string Run = "Run";
    protected const string Walk_Left = "Walk_Left";
    protected const string Walk_Right = "Walk_Right";
    protected const string Walk_Up = "Walk_Up";
    protected const string Walk_Up_Left = "Walk_Up_Left";
    protected const string Walk_Up_Right = "Walk_Up_Right";
    protected const string Walk_Down = "Walk_Down";
    protected const string Walk_Down_Left = "Walk_Down_Left";
    protected const string Walk_Down_Right = "Walk_Down_Right";
    protected const string diabloAnim = "diablo";

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }//end of awake

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }//end of start

    protected virtual void Update()
    {
        //----------------HAREKET---------------
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (distanceToPlayer <= detectionRange && !isDeath)
        {
            moving = true;
            // Player'a doðru olan yönü hesapla
            directionToPlayer = (target.position - transform.position).normalized;
            // Npc'yi hesaplanmýþ yöne doðru hareket ettir.
                rb.velocity = directionToPlayer * moveSpeed;
        }
        else
        {
            // Player range içinde deðilse hareket etmeyi kes.
            moving = false;
            rb.velocity = Vector2.zero;
        }
    }//end of update

    protected virtual void FixedUpdate()
    {
        goDirection = this.transform.position - target.transform.position;

            if (goDirection.x >= 0.1f && goDirection.y == 0 && moving)
                ChangeAnimationState(Walk_Left);
            else if (goDirection.x <= -0.1f && goDirection.y == 0 && moving)
                ChangeAnimationState(Walk_Right);
            else if (goDirection.x == 0 && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Down);
            else if (goDirection.x >= 0.1f && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Down_Left);
            else if (goDirection.x <= -0.1f && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Down_Right);
            else if (goDirection.x == 0 && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Up);
            else if (goDirection.x >= 0.1f && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Up_Left);
            else if (goDirection.x <= -0.1f && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Up_Right);
            else if (!moving)
                ChangeAnimationState(Idle);

    }//end of FixedUpdate

    protected void ChangeAnimationState(string newState)
    {
        //halihazýrda oynayan animasyonun kendini kesmesine engel ol
        if (currentState == newState)
            return;

        //yeni anim oynat

        animator.Play(newState);

        //current state güncelle çünkü deðiþti
        currentState = newState;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        StartCoroutine(ColorShift());

        if (currentHealth <= 0)  
            Die();

        Debug.Log("NPC takes damage. Current Health: " + currentHealth);
    }//end of TakeDamage

    private IEnumerator ColorShift()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.white;
    }//end of ColorShift

    public void ApplyKnockback(Vector2 KnockbackDirec, int knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(KnockbackDirec * knockbackForce, ForceMode2D.Impulse);
    }//end of ApplyKnockback

    protected virtual void Die()
    {
        isDeath = true;
        DropPoint = this.transform;
        Destroy(gameObject);
        Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
    }//end of Die
}//end of class
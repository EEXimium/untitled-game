using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    private Transform target;
    private Rigidbody2D rb;

    public GameObject deathCoin;
    private Transform DropPoint;

    private Animator animator;
    private SpriteRenderer spriterenderer;

    public float ExplodeDamage;
    public float ExplosionRange;

    [Header("Movement Settings")] //INSPECTOR'DAN AYARLAYIN! KODDA DEÐER GÝRMEYÝN!

    public float moveSpeed;
    public float detectionRange;
    public float shootRange;
    public float distanceToPlayer;
    public bool moving;

    [Header("Health Settings")] //INSPECTOR'DAN AYARLAYIN! KODDA DEÐER GÝRMEYÝN!
    public float maxHealth;
    public float currentHealth;
    public bool isDeath = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }//end of awake

    private void Start()
    {
        //----------------HEALTH------------------
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        //----------------HEALTH------------------
    }//end of start

    private void Update()
    {

        //----------------MOVEMENT---------------
        distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= detectionRange)
        {
            moving = true;
            // Player'a doðru olan yönü hesapla
            Vector2 directionToPlayer = (target.position - transform.position).normalized;
            // Npc'yi hesaplanmýþ yöne doðru hareket ettir.
            if (this.tag == "ExplosiveNPC" || this.tag == "SplitMOB") //Explosive ve split movementi
                rb.velocity = directionToPlayer * moveSpeed;
            else if (this.tag == "RangedNPC")                         //Ranged movementi
                rb.velocity = -directionToPlayer * moveSpeed;
        }
        else
        {
            // Player range içinde deðilse hareket etmeyi kes.
            moving = false;
            rb.velocity = Vector2.zero;
        }
        //----------------MOVEMENT END--------------

        //---------------ATTACK---------------------
        if (moving == false && distanceToPlayer <= shootRange)
            this.GetComponentInChildren<Gun>().rangedNpcAttack(); //Ranged attacký
        //---------------ATTACK END-----------------

    }//end of update

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

    private void Die()
    {
        isDeath = true;
        DropPoint = this.transform;
        if (this.tag == "ExplosiveNPC")
        {
            Explode();
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
        else if (this.tag == "SplitMOB")
        {
            this.gameObject.GetComponent<SplitOnDeath>().SpawnOnDeath();
            Explode();
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
    }//end of Die

    private void Explode()
    {
        GameObject deadExplosion = new GameObject("Dead explosion radius");
        deadExplosion.transform.position = this.transform.position;
        deadExplosion.AddComponent<CircleCollider2D>();
        deadExplosion.AddComponent<Explosion>();
        deadExplosion.GetComponent<Explosion>().damage = ExplodeDamage;
        deadExplosion.GetComponent<CircleCollider2D>().radius = ExplosionRange;
        deadExplosion.GetComponent<CircleCollider2D>().isTrigger = true;
        Destroy(deadExplosion, .1f);
        Destroy(gameObject);
    }//end of Explode

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (this.tag == "ExplosiveNPC" || this.tag == "SplitMOB"))
        {
            Die();
            Destroy(gameObject);
        }
    }//end of OnTriggerEnter2D

}//end of class
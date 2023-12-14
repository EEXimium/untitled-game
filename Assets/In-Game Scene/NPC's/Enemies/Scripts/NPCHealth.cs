using System.Collections;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;

    public float ExplodeDamage;
    public float ExplosionRange;

    public GameObject deathCoin;
    private Transform DropPoint;

    private Animator animator;
    private SpriteRenderer spriterenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0) { return; }

        currentHealth -= damage;
        StartCoroutine(ColorShift());

        if (currentHealth <= 0) { Die(); }

        Debug.Log("NPC takes damage. Current Health: " + currentHealth);
    }
    private IEnumerator ColorShift()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriterenderer.color = Color.white;
    }

    public void ApplyKnockback(Vector2 KnockbackDirec, int knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;     
        rb.AddForce(KnockbackDirec * knockbackForce, ForceMode2D.Impulse);
    }

    private void Die()
    {
        DropPoint = this.transform;
        if(this.tag == "ExplosiveNPC")
        {
            Explode();
        }
        else
        {
            Destroy(gameObject);
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
    }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.tag == "ExplosiveNPC")
        {
            Die();
            Destroy(gameObject);
        }
    }
}

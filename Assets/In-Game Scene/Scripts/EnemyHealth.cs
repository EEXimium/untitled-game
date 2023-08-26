using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D Pcoll;
    private SpriteRenderer SR;
    public Transform Weapon;

    public int bossmaxHealth = 214;
    public int bosscurrentHealth;
    public bool BossAlive = true;

    [SerializeField] private Transform Boss;
    [SerializeField] private Color newColor = Color.red;


    void Start()
    {
        bosscurrentHealth = bossmaxHealth;

        rb = GetComponent<Rigidbody2D>();
        Pcoll = GetComponent<PolygonCollider2D>();
        SR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!BossAlive)
        {
            return;
        }
    }

    public void BossTakeDamage(int damage)
    {
        bosscurrentHealth -= damage;

        Debug.Log($"Remaining health of enemy: {bosscurrentHealth}");

        if (bosscurrentHealth <= 0)
        {
            StartCoroutine(ColorShift());
            Die();
        }

        if (BossAlive)
        {
            StartCoroutine(ColorShift());
        }
    }
    public void EnemyKnockback(Vector2 KnockbackDirec, int knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;  // Reset any existing velocity       
        rb.AddForce(KnockbackDirec * knockbackForce, ForceMode2D.Impulse);
    }

    private IEnumerator ColorShift()
    {
        SR.color = newColor;
        yield return new WaitForSeconds(0.3f);
        SR.color = Color.white;
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}

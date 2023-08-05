using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 20f;
    public int damage = 10;
    public float Explosiontimer = 3f;
    private CircleCollider2D Ccoll;

    void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(Explosion());
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Ccoll.radius = 0.8f;
    }

    private void OnTriggerStay2D (Collider2D collision)
    {

        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null && enemy.BossAlive)
        {
            enemy.BossTakeDamage(damage);
        }
        Destroy(gameObject);
    }


    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Explosiontimer);
        Ccoll.radius = 0.8f;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}

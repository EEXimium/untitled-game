using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 20f;
    public int damage = 10;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Npc enemy = collision.GetComponent<Npc>();
        PlayerHealth player = collision.GetComponent<PlayerHealth>();
        if (enemy != null && (collision.tag == "RangedNPC" || collision.tag == "ExplosiveNPC"))
        {
            enemy.TakeDamage(damage);
        }
        if(player != null && collision.tag == "Player")
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}

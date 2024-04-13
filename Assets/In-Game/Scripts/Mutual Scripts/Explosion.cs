using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            if(playerHealth != null)
            {
                Vector2 distance = collision.gameObject.transform.position - this.transform.position;
                playerHealth.TakeDamage(damage * distance.magnitude);
            }
        }

        if (collision.gameObject.CompareTag("ExplosiveNPC")/* || collision.gameObject.CompareTag("Enemy")*/)
        {
            Npc enemy = collision.GetComponent<Npc>();

            if (enemy != null)
            {
                Vector2 distance = collision.gameObject.transform.position - this.transform.position;
                enemy.TakeDamage(damage * distance.magnitude);
            }
        }

    }
}

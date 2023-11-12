using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class npcExplosion : MonoBehaviour
{

    public int damage = 10;
    public float Explosiontimer = 3f;
    private CircleCollider2D Ccoll;
    private CapsuleCollider2D Capscoll;
    private PlayerHealth playerHealth;
    private GameObject player;

    
    void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
        Capscoll= GetComponent<CapsuleCollider2D>();
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //StartCoroutine(Explosion());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ccoll.radius = 1.0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        //playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth.currentHealth != 0.0f && collision.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public IEnumerator Explosion()
    {
        yield return new WaitForSeconds(Explosiontimer);
        Ccoll.radius = 1.0f;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}

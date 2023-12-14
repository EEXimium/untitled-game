using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    private CircleCollider2D Ccoll;
    private Rigidbody2D rb;

    public float speed = 20f;   
    public float damage = 10;
    public float ExplosionRange = 2f;
    public float Explosiontimer = 3f;

    void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        Invoke("Explosion", Explosiontimer);
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        NPCHealth enemy = collision.GetComponent<NPCHealth>();
        if (enemy != null)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        GameObject GranadeExplosion = new GameObject("Granade explosion radius");
        GranadeExplosion.transform.position = this.transform.position;
        GranadeExplosion.AddComponent<CircleCollider2D>();
        GranadeExplosion.AddComponent<Explosion>();
        GranadeExplosion.GetComponent<Explosion>().damage = damage;
        GranadeExplosion.GetComponent<CircleCollider2D>().radius = ExplosionRange;
        GranadeExplosion.GetComponent<CircleCollider2D>().isTrigger = true;
        Destroy(GranadeExplosion, .1f);
        Destroy(gameObject);
    }

}

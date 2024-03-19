using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveNpc : Npc
{
    [Header("ExplosiveNpc Settings")]
    [SerializeField] protected float ExplodeDamage;
    [SerializeField] protected float ExplosionRange;

    protected override void Die()
    {
        if (this.tag == "ExplosiveNPC")
        {
            Explode();
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
    }

    protected void Explode()
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
            Destroy(gameObject);
        }
    }//end of OnTriggerEnter2D
}

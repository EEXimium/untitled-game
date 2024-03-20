using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnDeathNpc : Npc
{

    [Header("SplitNpc Settings")]
    [SerializeField] protected float ExplodeDamage;
    [SerializeField] protected float ExplosionRange;
    [SerializeField] private GameObject miniMOb;
    [SerializeField] private List<Transform> SpawnLocations = new List<Transform>();

    protected override void Die()
    {
        if (CompareTag("SplitMOB"))
        {
            SpawnOnDeath();
            Explode();
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
        }
    }

    protected void SpawnOnDeath()
    {
        for (int i = 0; i < SpawnLocations.Count; i++)
        {
            Instantiate(miniMOb, SpawnLocations[i].position, Quaternion.identity);
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

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
            Destroy(gameObject);
        }
    }//end of OnTriggerEnter2D

}//end of class

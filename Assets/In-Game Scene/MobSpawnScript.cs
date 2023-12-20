using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private List<Transform> SpawnLocations = new List<Transform>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpawnOnTrigger();
        }
    }
    private void SpawnOnTrigger()
    {      
        for (int i = 0; i < SpawnLocations.Count; i++)
        {
            int RandomEnemy = Random.Range(0, Enemies.Count);
            Instantiate(Enemies[RandomEnemy], SpawnLocations[i].position, Quaternion.identity);
        }
    }

}

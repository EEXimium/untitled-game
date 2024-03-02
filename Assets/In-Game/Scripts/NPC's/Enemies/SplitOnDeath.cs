using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject miniMOb;
    [SerializeField] private List<Transform> SpawnLocations = new List<Transform>();

    public void SpawnOnDeath()
    {
        for (int i = 0; i < SpawnLocations.Count; i++) 
        {
            Instantiate(miniMOb, SpawnLocations[i].position, Quaternion.identity);            
        }
    }
}

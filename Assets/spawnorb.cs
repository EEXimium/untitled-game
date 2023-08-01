using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnorb : MonoBehaviour
{
    public GameObject OrbHolder;

    public GameObject SuPrefab;
    public void Spawn()
    {
        Instantiate(SuPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);
    }
}

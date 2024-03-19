using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitOnDeathNpc : Npc
{
    protected override void Die()
    {
            this.gameObject.GetComponent<SplitOnDeath>().SpawnOnDeath();
            //Explode();
            Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
    }
}

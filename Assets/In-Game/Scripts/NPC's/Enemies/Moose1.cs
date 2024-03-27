using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose1 : Npc
{
    //--------------ANIMATION
    string moose1_run = "moose1_run";
    string moose1_idle = "moose1_idle";
    string moose1_atk = "moose1_atk";
    string moose1_death = "moose1_death";
    string moose1_hurt = "moose1_hurt";


    protected override void FixedUpdate()
    {
        goDirection = this.transform.position - target.transform.position;

        if (goDirection.x >= 0.1f && moving)
        {
            ChangeAnimationState(moose1_run);
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if (goDirection.x <= -0.1f && moving)
        {
            ChangeAnimationState(moose1_run);
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (!moving && !isDeath)
            ChangeAnimationState(moose1_idle); 
         if (isDeath)
        {
            ChangeAnimationState(moose1_death);
        }
    }
    protected override void Die()
    {
        //ChangeAnimationState(moose1_death);
        isDeath = true;
        DropPoint = this.transform;
        Instantiate(deathCoin, DropPoint.position, Quaternion.identity);
    }
    public void gameobjectdestroy()
    {
        Destroy(gameObject);
    }

    
}

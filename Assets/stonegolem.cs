using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stonegolem : Npc
{

    string stonegolem_run = "stone golem-run";
    string stonegolem_idle = "stone golem-idle";
    string stonegolem_atk = "stone golem-atk";
    string stonegolem_death = "stone golem-death";
    string stonegolem_hurt = "stone golem-hurt";

    [Header("Stone Golem settings")]

    [SerializeField] protected float hitRange;

    protected override void Update()
    {
        base.Update();
        if (distanceToPlayer < hitRange)
        {
            rb.velocity = Vector3.zero;
            moving = false;
            animator.Play(stonegolem_atk);
        }

    }

    protected override void FixedUpdate()
    {
        goDirection = this.transform.position - target.transform.position;

        if (goDirection.x >= 0.1f && moving)
        {
            ChangeAnimationState(stonegolem_run);
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if (goDirection.x <= -0.1f && moving)
        {
            ChangeAnimationState(stonegolem_run);
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
        else if (!moving && !isDeath)
            ChangeAnimationState(stonegolem_idle);
        if (isDeath)
        {
            ChangeAnimationState(stonegolem_death);
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

    public void attack()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void deactivateHit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

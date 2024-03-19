using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNpc : Npc
{
    [Header("RangedNpc Settings")]
    [SerializeField] protected float shootRange;
    protected override void Update()
    {
        base.Update();
        //--------------HAREKET-----------------
        if (distanceToPlayer <= detectionRange)
        {
            if (CompareTag("RangedNPC"))
                rb.velocity = -directionToPlayer * moveSpeed;
        }

        //---------------SALDIRI-----------------
        if (moving == false && distanceToPlayer <= shootRange)
            this.GetComponentInChildren<Gun>().rangedNpcAttack();
    }//end of update

    protected override void FixedUpdate()
    {
        goDirection = this.transform.position - target.transform.position;
        
            if (goDirection.x >= 0.1f && goDirection.y == 0 && moving)
                ChangeAnimationState(Walk_Right);
            else if (goDirection.x <= -0.1f && goDirection.y == 0 && moving)
                ChangeAnimationState(Walk_Left);
            else if (goDirection.x == 0 && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Up);
            else if (goDirection.x >= 0.1f && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Up_Right);
            else if (goDirection.x <= -0.1f && goDirection.y >= 0.1 && moving)
                ChangeAnimationState(Walk_Up_Left);
            else if (goDirection.x == 0 && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Down);
            else if (goDirection.x >= 0.1f && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Down_Right);
            else if (goDirection.x <= -0.1f && goDirection.y <= -0.1 && moving)
                ChangeAnimationState(Walk_Down_Left);
            else if (moving)
                ChangeAnimationState(Idle);
    }//end of FixedUpdate
}//end of class

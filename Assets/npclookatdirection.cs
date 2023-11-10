using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npclookatdirection : MonoBehaviour
{
    private string currentState;

    
    private GameObject npc;
    private Animator anim;
    private Transform target;
    private NPCMovement npcM;

    //ANIM STATES
    const string Idle = "Idle";
    const string Run = "Run";
    const string Walk_Left = "Walk_Left";
    const string Walk_Right = "Walk_Right";
    const string Walk_Up = "Walk_Up";
    const string Walk_Up_Left = "Walk_Up_Left";
    const string Walk_Up_Right = "Walk_Up_Right";
    const string Walk_Down = "Walk_Down";
    const string Walk_Down_Left = "Walk_Down_Left";
    const string Walk_Down_Right = "Walk_Down_Right";
    const string diabloAnim = "diablo";


    private void Start()
    {
        npc = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        npcM = GetComponent<NPCMovement>();
    }

    private void FixedUpdate()
    {

        Vector2 direction = this.transform.position - target.transform.position;


        //ANIM CHANGES

        if (direction.x >= 0.1f && direction.y == 0 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Left);
        }
        else if (direction.x <= -0.1f && direction.y == 0 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Right);
        }
        else if (direction.x == 0 && direction.y >= 0.1 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Down);
        }
        else if (direction.x >= 0.1f && direction.y >= 0.1 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Down_Left);
        }
        else if (direction.x <= -0.1f && direction.y >= 0.1 && !npcM.notMoving )
        {
            ChangeAnimationState(Walk_Down_Right);
        }
        else if (direction.x == 0 && direction.y <= -0.1 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Up);
        }
        else if (direction.x >= 0.1f && direction.y <= -0.1 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Up_Left);
        }
        else if (direction.x <= -0.1f && direction.y <= -0.1 && !npcM.notMoving)
        {
            ChangeAnimationState(Walk_Up_Right);
        }
        else if (npcM.notMoving)
        {
            ChangeAnimationState(Idle);
        }

        
    }



    void ChangeAnimationState(string newState)
    {
        //halihazýrda oynayan animasyonun kendini kesmesine engel ol
        if (currentState == newState)
            return;

        //yeni anim oynat
        
        anim.Play(newState);
        
;
        //current state güncelle çünkü deðiþti
        currentState = newState;
    }

}

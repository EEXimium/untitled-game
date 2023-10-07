using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLookAtDirection : MonoBehaviour
{
    private string currentState;

    public PlayerMovement plmov;

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

    private void FixedUpdate()
    {

        //ANIM CHANGES

        if (plmov.InputVector.x >= 0.1f && plmov.InputVector.y == 0)
        {
            ChangeAnimationState(Walk_Right);
        }
        else if (plmov.InputVector.x <= -0.1f && plmov.InputVector.y == 0)
        {
            ChangeAnimationState(Walk_Left);
        }
        else if (plmov.InputVector.x == 0 && plmov.InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up);
        }
        else if (plmov.InputVector.x >= 0.1f && plmov.InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up_Right);
        }
        else if (plmov.InputVector.x <= -0.1f && plmov.InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up_Left);
        }
        else if (plmov.InputVector.x == 0 && plmov.InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down);
        }
        else if (plmov.InputVector.x >= 0.1f && plmov.InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down_Right);
        }
        else if (plmov.InputVector.x <= -0.1f && plmov.InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down_Left);
        }
        else
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
        plmov.animator.Play(newState);

        //current state güncelle çünkü deðiþti
        currentState = newState;
    }

}

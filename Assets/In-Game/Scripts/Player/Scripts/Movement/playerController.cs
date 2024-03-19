using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement plmov;

    // LOOK AT MOUSE //
    [SerializeField] private GameObject Crosshair;

    private Vector2 aim;
    private Vector2 direction;
    private Vector2 MainAim;
    // ------------------------------------------- //

    private string currentState;


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
        anim = GetComponent<Animator>();
        plmov = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!Input.GetMouseButton(1))
        {
            GamepadAim();
        }
        if (aim.magnitude <= 0f)
        {
            MouseAim();
        }
        if (!Input.GetMouseButton(1) && aim.magnitude <= 0f)
            LookatDirection();


    }
    void FixedUpdate() //animasyon deðiþiklikleri fixedupdate - teknik deðiþikler normal update
    {
        // LOOK AT MOUSE //
        anim.SetFloat("MouseHorizontal", MainAim.x);
        anim.SetFloat("MouseVertical", MainAim.y);
        anim.SetFloat("Speed", plmov.InputVector.sqrMagnitude);
        // ------------------------------------------- //
    }

    private void GamepadAim()
    {
        aim = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
        if (aim.magnitude > 0f)
        {
            anim.SetLayerWeight(1, 1);
            aim.Normalize();
            aim *= 2f;
            Crosshair.SetActive(true);
            Crosshair.transform.localPosition = aim;        
            MainAim = aim;
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            Crosshair.SetActive(false);
            Crosshair.transform.localPosition = this.transform.position;
        }
    }
    private void MouseAim()
    {
        //------Look at cursor--------
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 characterPosition = transform.position;
        direction = cursorPosition - characterPosition;

        direction = Vector2.ClampMagnitude(direction, 1f);
        direction = new Vector2(Mathf.Clamp(direction.x, -1f, 1f), Mathf.Clamp(direction.y, -1f, 1f));
       
        if (direction.magnitude > 0f && Input.GetMouseButton(1))
        {
            anim.SetLayerWeight(1, 1);
            Crosshair.SetActive(true);
            direction *= 2f;
            Crosshair.transform.localPosition = direction;
            MainAim = direction;
        }
        else
        {
            anim.SetLayerWeight(1, 0);
            Crosshair.SetActive(false) ;
        }

    }
    private void LookatDirection()
    {
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
        anim.SetLayerWeight(1, 0);
        //halihazýrda oynayan animasyonun kendini kesmesine engel ol
        if (currentState == newState)
            return;

        //yeni anim oynat
        anim.Play(newState);

        //current state güncelle çünkü deðiþti
        currentState = newState;
    }
}

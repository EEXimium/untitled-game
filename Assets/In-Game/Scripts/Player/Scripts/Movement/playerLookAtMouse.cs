using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class playerLookAtMouse : MonoBehaviour
{
    public PlayerMovement plmov;
    [SerializeField] private GameObject Crosshair;

    private Vector2 aim;
    private Vector2 direction;
    private Vector2 MainAim;

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
        
    }
    void FixedUpdate() //animasyon deðiþiklikleri fixedupdate - teknik deðiþikler normal update
    {

        plmov.animator.SetFloat("MouseHorizontal", MainAim.x);
        plmov.animator.SetFloat("MouseVertical", MainAim.y);
        plmov.animator.SetFloat("Speed", plmov.InputVector.sqrMagnitude);
    }

    private void GamepadAim()
    {
        aim = new Vector2(Input.GetAxisRaw("AimHorizontal"), Input.GetAxisRaw("AimVertical"));
        if (aim.magnitude > 0f)
        {
            aim.Normalize();
            aim *= 2f;
            Crosshair.SetActive(true);
            Crosshair.transform.localPosition = aim;        
            MainAim = aim;
        }
        else
        {
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
            Crosshair.SetActive(true);
            direction *= 2f;
            Crosshair.transform.localPosition = direction;
            MainAim = direction;
        }
        else
        {
            Crosshair.SetActive(false) ;
        }

    }
}

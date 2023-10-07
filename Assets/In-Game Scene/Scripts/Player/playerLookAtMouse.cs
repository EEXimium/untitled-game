using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLookAtMouse : MonoBehaviour
{
    public PlayerMovement plmov;


    void FixedUpdate() //animasyon deðiþiklikleri fixedupdate - teknik deðiþikler normal update
    {
        //------Look at cursor--------
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 characterPosition = transform.position;
        Vector2 direction = cursorPosition - characterPosition;

        direction = Vector2.ClampMagnitude(direction, 1f);
        direction = new Vector2(Mathf.Clamp(direction.x, -1f, 1f), Mathf.Clamp(direction.y, -1f, 1f));

        //-----------------------------------------------

        plmov.animator.SetFloat("MouseHorizontal", direction.x);
        plmov.animator.SetFloat("MouseVertical", direction.y);
        plmov.animator.SetFloat("Speed", plmov.InputVector.sqrMagnitude);
    }
}

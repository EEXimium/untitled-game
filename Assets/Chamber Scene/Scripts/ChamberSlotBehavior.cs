using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChamberSlotBehavior : MonoBehaviour
{
    private CircleCollider2D Ccoll;
    private Animator anim;

    void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ActiveChamber"))
        {
            anim.SetBool("start", true);
            Ccoll.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ActiveChamber"))
        {
            anim.SetBool("start", false);
            Ccoll.isTrigger = false;
        }
    }

}

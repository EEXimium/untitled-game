using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{

    private Collider2D boxCollider;
    private Animator anim;
    private bool ChestOpen = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<Collider2D>();
    }
    public void OpenChest()
    {
        anim.SetBool("Open", true);

    }

    public void CloseChest()
    {
        anim.SetBool("Open", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && ChestOpen)
        {
            if (anim.GetBool("Open"))
            {
                CloseChest();
            }
            else
            {
                OpenChest();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChestOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChestOpen = false;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator anim;
    private bool DoorButtonActive = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        anim.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        anim.SetBool("Open", false);
    }
 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && DoorButtonActive) {
            if (anim.GetBool("Open"))
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
         }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorButtonActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorButtonActive = false;
        }
    }
  
    }



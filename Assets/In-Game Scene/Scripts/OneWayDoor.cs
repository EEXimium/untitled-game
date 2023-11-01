using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    public NPCHealth NPCHealth;
    public Animator anim;
    
    private void Update()
    {
        if (NPCHealth.currentHealth <= 0) 
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            anim.SetTrigger("DoorOpen");
        }
    }
}

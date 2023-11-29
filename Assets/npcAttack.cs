using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Gun gun;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {

        if (this.GetComponent<NPCMovement>().moving == false
            && this.GetComponent<NPCMovement>().distanceToPlayer <= this.GetComponent<NPCMovement>().shootRange)
        {
            //this.transform.GetChild(0).GetComponent<Gun>().shootPrimary();
            this.GetComponent<Gun>().shootPrimary();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public GameObject canvasToOpen;
    private GameObject canvasToClose;

    private void Start()
    {
        canvasToClose = GameObject.Find("Canvas");
    }
    private void OnTriggerStay2D(Collider2D other) //Player NPC'nin colliedrý içinde ise F tuþuna basýlýp shop açýlmasýný saðlýyor.
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F)) 
        {
            canvasToOpen.SetActive(true); 
            canvasToClose.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToOpen.SetActive(false);
            canvasToClose.SetActive(true);
        }
    }
}

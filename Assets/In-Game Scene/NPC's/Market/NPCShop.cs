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
    private void OnTriggerStay2D(Collider2D other) //Player NPC'nin colliedr� i�inde ise F tu�una bas�l�p shop a��lmas�n� sa�l�yor.
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

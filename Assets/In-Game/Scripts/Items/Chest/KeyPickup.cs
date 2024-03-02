using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();

            if (playerInventory != null)
            {
                playerInventory.AddKey();
                // Deactivate the key GameObject.
                gameObject.SetActive(false);
            }
        }
    }
}

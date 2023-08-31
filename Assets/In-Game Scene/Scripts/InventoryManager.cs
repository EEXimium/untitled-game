using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public GameObject InventoryMenu; 
    private bool menuActivated;
    public ItemSlot[] itemSlot; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
           Time.timeScale = 1; //Allows user to open inventory while game is active and running.
           InventoryMenu.SetActive(false);
           DeselectAllSlots(); //Deselects the selected slot after the menu is closed
           menuActivated = false;
        }
        
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
           Time.timeScale = 0; //Pauses the game while inventory is active.
           InventoryMenu.SetActive(true);
           menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
       Debug.Log("itemName: " + itemName + " quantity: " + quantity + " itemSprite: " + itemSprite);

       for (int i = 0; i < itemSlot.Length; i++)
       {
           if(itemSlot[i].isFull == false)
           {
                itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                return;
           }
       }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}

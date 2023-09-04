using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public GameObject InventoryMenu; 
    private bool menuActivated;
    public ItemSlot[] itemSlot; 

    public ItemSO[] itemSOs;
    
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

    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if(itemSOs[i].itemName == itemName)
            {
                itemSOs[i].UseItem();
            }
        }
    }
    
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
       Debug.Log("itemName: " + itemName + " quantity: " + quantity + " itemSprite: " + itemSprite);

       for (int i = 0; i < itemSlot.Length; i++)
       {
           if(itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].quantity == 0 )
           {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);
                
                return leftOverItems;
           }
       }
        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
            itemSlot[i].ItemDescriptionNameText.text = null;
            itemSlot[i].ItemDescriptionText.text = null;
            itemSlot[i].ItemDesccriptionImage.sprite = null;
        }
    }

}

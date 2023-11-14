using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    //-----------Item Data---------------
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    //------------------------------------

    [SerializeField]
    private int maxNumberOfItems;

    //-----------Item Slot---------------
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;
    //------------------------------------

    //-----------Item Description Slot---------------
    public Image ItemDesccriptionImage;
    public TMP_Text ItemDescriptionNameText;
    public TMP_Text ItemDescriptionText;
    //-----------------------------------------------

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //Checking if the slot is full or not
        if (isFull)
            return quantity;

        
        //update item specs
        this.itemName = itemName;
        
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        itemImage.enabled = true;
        
        this.itemDescription = itemDescription;
        
        this.quantity += quantity;
        if(this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;
            
            //Returning leftover items
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        //Update quantity text
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;           
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
            Debug.Log("Sol");
        }
        
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
            Debug.Log("Sað");
        }
    }

    public void OnLeftClick()
    {
        if(thisItemSelected)
        {
            inventoryManager.UseItem(itemName);
            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();
            Debug.Log("Used item " + itemName);

            if(this.quantity <= 0)
                EmptySlot();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            ItemDescriptionNameText.text = itemName;
            ItemDescriptionText.text = itemDescription;
            ItemDesccriptionImage.sprite = itemSprite;
        }
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = null;
        itemImage.color = new Color(255, 255, 255, 130);
        ItemDescriptionNameText.text = null;
        ItemDescriptionText.text = null;
        ItemDesccriptionImage.sprite = null;
    }


    public void OnRightClick()
    {
        //creating a new item when dropped
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        //creating and modifying sprite renderer so it is visually the same
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
    //  sr.sortingLayerName = "LayerName"; (This is for seperate layers to create on the item)

        //adding a collider for interaction
        itemToDrop.AddComponent<BoxCollider2D>();

        //setting the location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1f, 0, 0);

        //subtracting the quantity of the dropped item
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();
        Debug.Log("Dropped item " + itemName);

            if(this.quantity <= 0)
                EmptySlot();
    }

}

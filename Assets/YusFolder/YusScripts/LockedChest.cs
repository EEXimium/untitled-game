using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedChes : MonoBehaviour
{
    private Collider2D boxCollider;
    private Animator anim;
    [SerializeField] private bool ChestOpen = false; //chestopen is actually check if chest can be oppenable or not
    public GameObject itemToDrop;
    [SerializeField] private bool ChestUsed = true; //if true not oppened if false is oppened
    public Transform DropPoint;
    // locked chest codes
    [SerializeField] private bool isLocked = true;
    public GameObject key;
    private PlayerInventory playerInventory;
    private void Awake()
    {
        Vector3 childPosition = DropPoint.position;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<Collider2D>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    public void OpenChest()
    {
        Instantiate(itemToDrop, DropPoint.position, Quaternion.identity);
        anim.SetBool("Open", true);

    }

    public void CloseChest()
    {
        anim.SetBool("Open", false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && ChestOpen && ChestUsed)
        {
            if (anim.GetBool("Open"))
            {
                CloseChest();
            }
            else
            {
                if (!isLocked || (isLocked && PlayerHasKey()))
                {
                    OpenChest();
                    ChestUsed = false;
                }
                else
                {
                    // Display a message to the player that the chest is locked.
                    Debug.Log("The chest is locked. You need a key to open it.");
                }
            }
        }

    }
    private bool PlayerHasKey()
    {
        // Check if the player has the key in their inventory.
        // You can implement your own logic here to check for the key.
        return playerInventory != null && playerInventory.HasKey;
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

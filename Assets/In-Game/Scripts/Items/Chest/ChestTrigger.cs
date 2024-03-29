using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{

    private Collider2D CircleCollider;
    private Animator anim;
    [SerializeField] private bool ChestOpen = false; //chestopen is actually check if chest can be oppenable or not
    public GameObject itemToDrop;
    [SerializeField] private bool ChestUsed = true; //if true not oppened if false is oppened
    public Transform DropPoint;
    private void Awake()
    {
        Vector3 childPosition = DropPoint.position;
        anim = GetComponent<Animator>();
        CircleCollider = GetComponent<CircleCollider2D>();
    }
    public void OpenChest()
    {
        Instantiate(itemToDrop, DropPoint.position, Quaternion.identity);
        anim.SetBool("Open", true);
        CircleCollider.enabled = false;

    }

    public void CloseChest()
    {
        anim.SetBool("Open", false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && ChestOpen && ChestUsed)
        {
            if (anim.GetBool("Open"))
            {
                CloseChest();
            }
            else
            {
                OpenChest();
                ChestUsed = false;
            }
        }

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

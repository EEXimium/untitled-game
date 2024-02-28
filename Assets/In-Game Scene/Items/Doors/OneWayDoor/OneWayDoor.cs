using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : MonoBehaviour
{
    private Animator anim;
    private BoxCollider Bcoll;
    private bool collided = false ;
    [SerializeField] private InsantiateText InsText;
    [SerializeField] private GameObject ChamberCanvas;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Bcoll = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (collided && Input.GetKey(KeyCode.E)) 
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
            ChamberCanvas.SetActive(true);
            anim.SetTrigger("DoorOpen");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = true;
            InsText.DisplayText(this.transform, new Vector3(0, 2, 0), Quaternion.identity, 5f, "Press 'E' for Go Chambers");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = false;
        }
    }
}

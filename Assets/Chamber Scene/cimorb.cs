using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cimorb : MonoBehaviour
{
    private bool canMove;
    private bool dragging;
    private CircleCollider2D Ccoll;

    public GameObject runrock;

    public GameObject CimPrefab;

    private void Start()
    {
        canMove = false;
        dragging = false;
        Ccoll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Ccoll == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
            else
            {
                canMove = false;
            }
            if (canMove)
            {
                dragging = true;
            }
        }
        if (dragging)
        {
            this.transform.position = mousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            canMove = false;
            dragging = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slot") && !dragging)
        {
            Destroy(runrock);
            Instantiate(CimPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}

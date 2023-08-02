using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehavior : MonoBehaviour
{
    private bool dragging;
    private CircleCollider2D Ccoll;

    [SerializeField] private Vector3 AnchorPoint;

    public GameObject SelfOrb;
    public GameObject ChamberPrefab;

    private void Start()
    {
        dragging = false;
        Ccoll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  // Get mouse pos

        if (Input.GetMouseButtonDown(0))
        {
            if (Ccoll == Physics2D.OverlapPoint(mousePos))  // if mouse pos on collider
            {
                dragging = true;
            }
        }

        if (dragging)
        {
            this.transform.position = mousePos;         // obje ==> mouse pos
        }
        else
        {
            StartCoroutine(ReturnToBase());      // return holder
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)        // Orb Creating Chamber
    {
        if (collision.gameObject.CompareTag("Slot") && !dragging)
        {
            Destroy(SelfOrb);
            Instantiate(ChamberPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ReturnToBase()
    {
        yield return new WaitForSeconds(.05f);
        this.transform.position = AnchorPoint;
    }
}

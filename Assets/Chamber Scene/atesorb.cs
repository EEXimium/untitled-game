using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atesorb : MonoBehaviour
{
    private bool dragging;
    private CircleCollider2D Ccoll;

    [SerializeField] private Vector3 AnchorPoint;

    public GameObject runrock;

    public GameObject AtesPrefab;

    private void Start()
    {
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
                dragging = true;
            }
        }

        if (dragging)
        {
            this.transform.position = mousePos;
        }
        else
        {
            StartCoroutine(ReturnToBase());
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slot") && !dragging)
        {
            Destroy(runrock);
            Instantiate(AtesPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator ReturnToBase()
    {
        yield return new WaitForSeconds(.05f);
        this.transform.position = AnchorPoint;
    }
}

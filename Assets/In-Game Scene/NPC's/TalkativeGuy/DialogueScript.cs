using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    private CircleCollider2D Ccoll;
    private bool collided = false;
    [SerializeField] private InsantiateText InsText;
    [SerializeField] private GameObject DialogueCanv;

    private void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        if (collided && Input.GetKey(KeyCode.E))
        {
            DialogueCanv.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = true;
            InsText.DisplayText(this.transform, new Vector3(0, 1, 0), Quaternion.identity, 2f, "'E' for talk");
            // players rigidbody needs to be changed to kinetic in this line till the dialogues end.
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

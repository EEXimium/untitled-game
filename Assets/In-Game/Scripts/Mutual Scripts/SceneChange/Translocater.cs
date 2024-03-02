using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Translocater : MonoBehaviour
{
    private bool collided = false;
    private InsantiateText InsText;
    private GameObject Player;
    public Transform Target;

    [SerializeField] private string text;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        InsText = GameObject.Find("Insantiate Text (empty)").GetComponent<InsantiateText>();
    }

    private void Update()
    {
        if (collided && Input.GetKeyDown(KeyCode.F))
        {
            Translocate(Target);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = true;
            InsText.DisplayText(this.transform, new Vector3(0, 2, 0), Quaternion.identity, 5f, text);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = false;
        }
    }

    private void Translocate(Transform newlocation)
    {
        Player.transform.position = newlocation.position;
    }
}

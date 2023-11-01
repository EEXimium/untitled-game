using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChamberSceneLoader : MonoBehaviour
{
    private bool collided = false;
    public InsantiateText InsText;

    private void Update()
    {
        if (collided && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = true;
            InsText.DisplayText(this.transform, new Vector3(0, 2, 0), Quaternion.identity, 5f, "Press 'F' for Go ChamberScene Falan");
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

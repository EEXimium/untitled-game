using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool collided = false;
    public InsantiateText InsText;

    private void Start()
    {
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
        //DontDestroyOnLoad(GameObject.Find("Canvas"));
    }

    private void Update()
    {
        if (collided && Input.GetKeyDown(KeyCode.F)) 
        {
            SceneLoad();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = true;
            InsText.DisplayText(this.transform, new Vector3 (0,2,0), Quaternion.identity, 5f, "Press 'F' for Go Chambers");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collided = false;
        }
    }
    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(53, 22, 0);
    }
}

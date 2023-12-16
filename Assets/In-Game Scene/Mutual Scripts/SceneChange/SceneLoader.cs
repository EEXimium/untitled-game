using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool collided = false;
    public InsantiateText InsText;
    [SerializeField] private DataPersistenceManager dataPersistenceManager;

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
    public enum Scenes 
    {
        Outside = 0,
        ToxicScene = 1, 
        ChamberScene = 2
    }
    public Scenes ChooseScene;
    public void SceneLoad()
    {
        SceneManager.LoadScene((int)ChooseScene);
        try
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
            dataPersistenceManager.SaveGame();
        }
        catch (System.Exception)
        {
            return;
        }
        
    }
}

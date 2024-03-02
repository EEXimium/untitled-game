using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //[SerializeField] private DataPersistenceManager dataPersistenceManager;

    private void Start()
    {
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Player"));
        //DontDestroyOnLoad(GameObject.Find("Canvas"));
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
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0, 0, 0);
        SceneManager.LoadScene((int)ChooseScene);
        //dataPersistenceManager.SaveGame();
        
    }
}

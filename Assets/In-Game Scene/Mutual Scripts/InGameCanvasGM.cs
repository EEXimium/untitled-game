using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameCanvasGM : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject HUD;

    [SerializeField] private DataPersistenceManager dataPersistenceManager;

    private void Update()
    {
        // ESC tuþu ile oyunu durdur.
        if (Input.GetKey(KeyCode.Escape))
        {
            FreezeGame();
            PauseMenu.SetActive(true);
            HUD.SetActive(false);
        }
    }
    public void FreezeGame()  { Time.timeScale = 0; }
    public void UnFreezeGame() { Time.timeScale = 1; }

    public void SaveGame()
    {
        dataPersistenceManager.SaveGame();
    }
    
    public enum Scenes { MainMenu, Outside }
    public Scenes ChooseScene;
    public void LoadScene()
    {
        SceneManager.LoadScene(ChooseScene.ToString());
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void NewGame()
    {
        dataPersistenceManager.NewGame();
    }
}

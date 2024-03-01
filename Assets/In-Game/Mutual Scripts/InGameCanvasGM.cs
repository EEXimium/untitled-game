using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameCanvasGM : MonoBehaviour
{
    //[Header("Menu Navigation")]
    //[SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    //[SerializeField] private Button newGameButton;
    //[SerializeField] private Button continueGameButton;
    //[SerializeField] private Button LoadGameButton;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private GameObject Player;


    private void Update()
    {
        // ESC tuþu ile oyunu durdur.
        if (Input.GetKey(KeyCode.Escape))
        {
            FreezeGame();
            PauseMenu.SetActive(true);
            HUD.SetActive(false);
        }

        if (Player.GetComponent<PlayerHealth>().isDead)
        {
            DeathMenu.SetActive(true);
        }


    }
    public void FreezeGame()  { Time.timeScale = 0; }
    public void UnFreezeGame() { Time.timeScale = 1; }
    
    public enum Scenes { MainMenu, Outside }
    public Scenes ChooseScene;
    public void LoadScene()
    {
        SceneManager.LoadScene(ChooseScene.ToString());
    }

    
}

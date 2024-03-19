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
    private GameObject HUD;
    [SerializeField] private GameObject DeathMenu;
    private GameObject Player;
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    private void Start()
    {
        HUD = GameObject.Find("HUD");
        Player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        // ESC tuþu ile oyunu durdur.
        if (Input.GetButtonDown("Pause"))
        {
            FreezeGame();
            PauseMenu.SetActive(true);
        }

        if (Player.GetComponent<PlayerHealth>().isDead)
        {
            FreezeGame();
            DeathMenu.SetActive(true);
        }
    }

    public void returnToSave()
    {
        DeathMenu.SetActive(false);
        saveSlotsMenu.ActivateMenu(true);
        
    }

    public void FreezeGame() { Time.timeScale = 1; HUD.SetActive(false); }

    public void UnFreezeGame() { Time.timeScale = 1; HUD.SetActive(true); }
    
    public enum Scenes { MainMenu, Outside }
    public Scenes ChooseScene;
    public void LoadScene()
    {
        SceneManager.LoadScene(ChooseScene.ToString());
    }

    
}

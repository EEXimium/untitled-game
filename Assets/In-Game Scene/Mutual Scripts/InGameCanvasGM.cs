using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameCanvasGM : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button LoadGameButton;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject HUD;


    [SerializeField] private DataPersistenceManager dataPersistenceManager;

    private void Start()
    {
        DisableButtonsDependingOnData();
    }
    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.Instance.HasGameData())
        {
            continueGameButton.interactable = false;
            LoadGameButton.interactable = false;
        }
    }

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
        saveSlotsMenu.ActivateMenu(false);
        this.DeactivateMenu();
    }

    public void OnLoadGameClicked()
    {
        saveSlotsMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    public void Continue()
    {
        //Load the next Scene - which will in turn load the game because of
        //OnSceneLoaded() in the datapersistencemanager

        SceneManager.LoadSceneAsync("Outside");
    }
    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
    }
    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}

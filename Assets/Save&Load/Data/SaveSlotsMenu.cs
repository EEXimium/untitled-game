using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenuGM mainMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button backButon;

    private SaveSlots[] saveSlots;

    private bool isLoadingGame = false; 

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
    }

    public void OnSaveSlotClicked(SaveSlots saveSlots)
    {
        //disable menu buttons
        DisableMenuButtons();

        //update the seleced profile id to be used for data persistence
        DataPersistenceManager.Instance.ChangeSelectedProfileId(saveSlots.GetProfileId());

        if (!isLoadingGame)
        {
            //create a new game - which will initialize our data to a clean slate 
            DataPersistenceManager.Instance.NewGame();
        }

        //Load the scene - which will turn save the game because of OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Outside");
    }
    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        // set this menu active
        this.gameObject.SetActive(true); 
        // set mode
        this.isLoadingGame = isLoadingGame;

        //Load all of the profiles that exists
        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.Instance.GetAllProfilesGameData();
        // Loop through each save slot in the UI and set the content appropriately
        foreach(SaveSlots saveSlots in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlots.GetProfileId(), out profileData);
            saveSlots.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlots.SetInteractable(false);
            }
            else
            {
                saveSlots.SetInteractable(true);
            }
        }
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
    public void DisableMenuButtons()
    {
        foreach (SaveSlots saveSlots in saveSlots)
        {
            saveSlots.SetInteractable(false);
        }
        backButon.interactable = false;
    }
}

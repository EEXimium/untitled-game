using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Data.Common;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;

    [Header("File Stroge Config")]
    [SerializeField] private string filename;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    private string selectedProfileId = "";
    public static DataPersistenceManager Instance { get; private set; } //süslü parantez scriptin public olmasýný ancak sadece private olarak düzenlenebilmesini saðlýyor.

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Found more han one data persistence manager in the scene.Destroying the newsest one.");
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        if (disableDataPersistence)
        {
            Debug.LogWarning("Data persistence is currently disabled!");
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, filename, useEncryption);
        this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;       
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void ChangeSelectedProfileId(string NewProfileId)
    {
        //update the profile to use for saving and loading
        this.selectedProfileId = NewProfileId;
        //load the game, which will use tha profile, updating our game data accordingly
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }
        //Load any saved data from a file using the data handler.
        this.gameData = dataHandler.Load(selectedProfileId);

        //start a new game data if the data is null and we're configured to initialize data for debugging purposes.
        if (this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }

        //if no data can be loaded, don't continue.
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A new game needs to be started before data can be loaded.");
            return;
        }

        //push the loaded data to all other script tha need it.
        foreach (IDataPersistence dataPersistenceObj in  dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //return right away if data persistence is disabled
        if (disableDataPersistence)
        {
            return;
        }
        // if we don't have any data to save,log a warning here.
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A new Game needs to be started before data can be saved");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        //timestamp the data to other scripts so they can update it
        gameData.lastUpdated = System.DateTime.Now.ToBinary();

        // save that data to a file using the data handler.
        dataHandler.Save(gameData, selectedProfileId);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}

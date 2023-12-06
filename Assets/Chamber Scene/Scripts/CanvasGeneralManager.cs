using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

public class CanvasGeneralManager : MonoBehaviour
{
    private EventSystem Event;
    //----------------- MAP - Cards Button --------------------
    [Header("Map-Cards Button Section")]
    [SerializeField] private int clickCount = 1;
    public GameObject Panel;
    public TextMeshProUGUI ButtonText;

    //----------------- Cards -------------------
    [Header("araba")]
    [SerializeField] 
    public GameObject CardSlot1;
    public GameObject CardSlot2;
    public GameObject CardSlot3;
    public List<GameObject> Cards;

    //----------------- ORBS -------------------
    [Header("Spawn Orb Section")]
    public GameObject CardChoice;
    [SerializeField] private GameObject OrbHolder;
    public List<GameObject> Orbs;
    private Dictionary<string, GameObject> OrbsDictionary = new Dictionary<string, GameObject>();
    private Transform cone;
    private bool executed;
    private void Start()
    {
        OrbHolder = GameObject.Find("orbHolder");
        Event = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        OrbsDictionary.Add("IceCard(Clone) (UnityEngine.GameObject)", Orbs[0]);
        OrbsDictionary.Add("NoirCard(Clone) (UnityEngine.GameObject)", Orbs[1]);
        OrbsDictionary.Add("ThunderCard(Clone) (UnityEngine.GameObject)", Orbs[2]);
        OrbsDictionary.Add("LavaCard(Clone) (UnityEngine.GameObject)", Orbs[3]);
        OrbsDictionary.Add("FoggyCard(Clone) (UnityEngine.GameObject)", Orbs[4]);
        OrbsDictionary.Add("ToxicCard(Clone) (UnityEngine.GameObject)", Orbs[5]);
        OrbsDictionary.Add("WindCard(Clone) (UnityEngine.GameObject)", Orbs[6]);
        OrbsDictionary.Add("ForrestCard(Clone) (UnityEngine.GameObject)", Orbs[7]);
        OrbsDictionary.Add("SteampunkCard(Clone) (UnityEngine.GameObject)", Orbs[8]);
        OrbsDictionary.Add("StockCard(Clone) (UnityEngine.GameObject)", Orbs[9]);
        OrbsDictionary.Add("CyberpunkCard(Clone) (UnityEngine.GameObject)", Orbs[10]);
    }
    private void Update()
    {
        if (Event.currentSelectedGameObject.name == "Info-SelectButton" && !executed && Event.currentSelectedGameObject != null)
        {
            SpawnOrbUniversal();
            executed = true;    
        }
        else if (Event.currentSelectedGameObject.name != "Info-SelectButton")
        {
            executed = false;
        }

    }

    public void OnClick()
    {
        clickCount++;
        if (clickCount % 2 == 0)
        {
            Panel.SetActive(false);  ButtonText.text = "CARDS";
        }
        else
        {
            Panel.SetActive(true);   ButtonText.text = "MAP";
        }
    }

    //----------------- Cards -------------------
    public void SpawnCard()
    {
        GameObject[] UsedCards = GameObject.FindGameObjectsWithTag("Cards");
        foreach (GameObject card in UsedCards)
        {
            Destroy(card);
        }

        int random1 = Random.Range(0, Cards.Count);
        GameObject Card1 = Instantiate(Cards[random1], CardSlot1.transform.position, Quaternion.identity);
        Card1.transform.SetParent(CardSlot1.transform);
        GameObject usedcard1 = Cards[random1];
        Cards.Remove(usedcard1);

        int random2 = Random.Range(0, Cards.Count);
        GameObject Card2 = Instantiate(Cards[random2], CardSlot2.transform.position, Quaternion.identity);
        Card2.transform.SetParent(CardSlot2.transform);
        GameObject usedcard2 = Cards[random2];
        Cards.Remove(usedcard2);

        int random3 = Random.Range(0, Cards.Count);
        GameObject Card3 = Instantiate(Cards[random3], CardSlot3.transform.position, Quaternion.identity);
        Card3.transform.SetParent(CardSlot3.transform);

        Cards.Add(usedcard1);
        Cards.Add(usedcard2);
    }

    //----------------- ORBS -------------------
    private string orbwhospawn;

    public void SpawnOrbUniversal()
    {
        Instantiate(OrbsDictionary[orbwhospawn], OrbHolder.transform.position, OrbHolder.transform.rotation, OrbHolder.transform);
    }
}

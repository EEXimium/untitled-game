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
    //private EventSystem Event;
    //----------------- MAP - Cards Button --------------------
    [Header("Map-Cards Button Section")]
    [SerializeField] private int clickCount = 1;
    [SerializeField] private GameObject Panel;
    [SerializeField] private TextMeshProUGUI ButtonText;

    //----------------- Cards -------------------
    [Header("Card Variables")]
    [SerializeField] private GameObject CardSlot1;
    [SerializeField] private GameObject CardSlot2;
    [SerializeField] private GameObject CardSlot3;
    [SerializeField] private List<GameObject> Cards;


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

}

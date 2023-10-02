using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class spawncards1 : MonoBehaviour
{
    private int clickCount = 1;
    public GameObject Panel;
    public TextMeshProUGUI ButtonText;

    //----------------- Cards -------------------
    public GameObject CardSlot1;
    public GameObject CardSlot2;
    public GameObject CardSlot3;

    public List<GameObject> Cards;

    public void OnClick()
    {
        clickCount++;

        if (clickCount % 2 == 0)
        {
            Panel.SetActive(false);
            ButtonText.text = "CARDS";
        }
        else
        {
            Panel.SetActive(true);
            ButtonText.text = "MAP";
        }
    }

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

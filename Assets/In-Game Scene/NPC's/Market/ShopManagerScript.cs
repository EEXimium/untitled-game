using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{

    public int[,] shopItems = new int[5,5]; //Shop'a koymak istedi�imiz max item say�s�n� artt�r�yor.

    private CoinCollector CoinCollector;
    [SerializeField] private TextMeshProUGUI GoldcoinsTxtShop;
    [SerializeField] private TextMeshProUGUI GoldCoinsTextCanvas;

    void Start()
    {
        CoinCollector = GameObject.FindWithTag("Player").GetComponent<CoinCollector>();

        //ID's
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        //Prices
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;

        //Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
    }


    public void Buy()
    {
        GameObject ButtonRef = GameObject.Find("EventSystem").GetComponent<EventSystem>().currentSelectedGameObject;

        if (CoinCollector.coinsCollected >= shopItems[2,ButtonRef.GetComponent<ButtonInfo>().ItemID])             // Gold coin miktar�n�n al�nmak isteyen itemden fazla ya da e�it olmas�n� kontrol ediyor.
        {
            CoinCollector.coinsCollected -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];            // Al�nan itemin de�erinin Gold coin say�s�ndan azalt�lmas�n� sa�l�yor.
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;                       // Al�nan itemin miktar�n�n artmas�n� sa�l�yor.
            GoldcoinsTxtShop.text = "GoldCoins:" + CoinCollector.coinsCollected.ToString();
            GoldCoinsTextCanvas.text = CoinCollector.coinsCollected.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }      
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCShop : MonoBehaviour
{
    public GameObject canvasToOpen;
    private GameObject canvasToClose;

    private CoinCollector CoinCollector;
    [SerializeField] private TextMeshProUGUI GoldcoinsTxtShop;
    [SerializeField] private TextMeshProUGUI GoldCoinsTextCanvas;

    private void Start()
    {
        canvasToClose = GameObject.Find("Canvas");

        CoinCollector = GameObject.FindWithTag("Player").GetComponent<CoinCollector>();
    }
    private void OnTriggerStay2D(Collider2D other) //Player NPC'nin colliedrý içinde ise F tuþuna basýlýp shop açýlmasýný saðlýyor.
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKey(KeyCode.F)) 
        {
            // Get Your Gold Coins Count
            Debug.Log(CoinCollector.coinsCollected);
            GoldcoinsTxtShop.text = "GoldCoins:" + CoinCollector.coinsCollected.ToString();

            canvasToOpen.SetActive(true); 
            canvasToClose.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvasToOpen.SetActive(false);
            canvasToClose.SetActive(true);
        }
    }
}

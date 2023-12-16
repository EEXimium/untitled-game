using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinsCollected;
    [SerializeField] private TextMeshProUGUI CoinsCountText;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
            CollectCoin(other.gameObject);
    }

    private void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        SetCoins(coinsCollected);
        Destroy(coin);
    }

    public void SetCoins(int count)
    {
        CoinsCountText.text = count.ToString();
    }
}


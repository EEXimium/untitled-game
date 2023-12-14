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

    void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        CoinsCountText.text = coinsCollected.ToString();
        Destroy(coin);
    }
}


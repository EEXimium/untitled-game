using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private int coinsCollected = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
            CollectCoin(other.gameObject);
    }

    void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        Debug.Log("Coin collected! Total coins collected: " + coinsCollected);
        Destroy(coin);
    }
}

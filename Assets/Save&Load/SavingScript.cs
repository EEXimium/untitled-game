using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingScript : MonoBehaviour , IDataPersistence
{

    private PlayerMovement PMovement ;
    private PlayerHealth PHealth;
    private PlayerConsumables PConsumables;
    private CoinCollector CCollector;
    //[SerializeField] private Transform Hand1;
    //[SerializeField] private Transform Hand2;

    private void Start()
    {
        PMovement = this.gameObject.GetComponent<PlayerMovement>();
        PHealth = this.gameObject.GetComponent<PlayerHealth>();
        PConsumables = this.gameObject.GetComponent<PlayerConsumables>();
        CCollector = this.gameObject.GetComponent<CoinCollector>();
    }
    public void LoadData(GameData data)
    {
        // Health
        this.PHealth.currentHealth = data.curentHealth;
        PHealth.SetHealth(data.curentHealth);

        // Position
        this.transform.position = data.playerPosition;

        // Health-Pot
        this.PConsumables.HealthPotCount = data.HealthPotCount;
        PConsumables.SetHealthPotCount(data.HealthPotCount);

        // Coins
        this.CCollector.coinsCollected = data.coinsCollected;
        CCollector.SetCoins(data.coinsCollected);

        // Weapons 
        //if (data.Hand1Weapon != null) Instantiate(data.Hand1Weapon.gameObject, Hand1);
        //if (data.Hand2Weapon != null) Instantiate(data.Hand2Weapon.gameObject, Hand2);
    }
    public void SaveData(ref GameData data)
    {
        // Health
        data.curentHealth = this.PHealth.currentHealth;

        // Position
        data.playerPosition = this.transform.position;

        // Health-Pot
        data.HealthPotCount = this.PConsumables.HealthPotCount;

        // Coins
        data.coinsCollected = this.CCollector.coinsCollected;

        //Weapons
        //if (Hand1.childCount > 0)  data.Hand1Weapon = Hand1.GetChild(0);
        //if (Hand2.childCount > 0)  data.Hand2Weapon = Hand2.GetChild(0);

    }


}

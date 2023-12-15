using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingScript : MonoBehaviour , IDataPersistence
{

    [SerializeField] private PlayerMovement PM ;
    [SerializeField] private PlayerHealth PH;
    [SerializeField] private PlayerConsumables PC;

    public void LoadData(GameData data)
    {
        this.PH.currentHealth = data.curentHealth;
        PH.SetHealth(data.curentHealth);

        this.transform.position = data.playerPosition;

        this.PC.HealthPotCount = data.HealthPotCount;
        PC.SetHealthPotCount(data.HealthPotCount);
    }
    public void SaveData(ref GameData data)
    {
        data.curentHealth = this.PH.currentHealth;
        data.playerPosition = this.transform.position;
        data.HealthPotCount = this.PC.HealthPotCount;
    }


}

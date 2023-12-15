using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour, IDataPersistence
{
    [SerializeField] TextMeshProUGUI HealthPotCountText;
    public PlayerHealth PH;
    public EffectMethods EM;
    private ShopManagerScript Shop;
       
    public int HealthPotCount = 2;

    private void Start()
    {
        HealthPotCountText.text = HealthPotCount.ToString();
    }

    //part of the Save&Load System
    public void LoadData(GameData data)
    {
        this.HealthPotCount = data.HealthPotCount;
    }
    public void SaveData(ref GameData data)
    {
        data.HealthPotCount = this.HealthPotCount;
    }
    private void Update()
    {
        // Speed buff
        if (Input.GetKeyDown(KeyCode.V) && !EM.SBActive)
        {
            StartCoroutine(EM.SpeedBuff());
        }

        // Extra HP
        if (Input.GetKeyDown(KeyCode.T) && !EM.ExtraHpActive)
        {
            StartCoroutine(EM.ExtraHp());
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && HealthPotCount > 0)
        {
            if (PH.currentHealth < PH.maxHealth)
            {
                EM.TakeHeal(2);
                HealthPotCount--;
                HealthPotCountText.text = HealthPotCount.ToString();
            }
        }
        try
        {
            Shop = GameObject.Find("ShopManager").GetComponent<ShopManagerScript>();

            if (Shop.shopItems[3, 1] > 0)
            {
                HealthPotCount += Shop.shopItems[3, 1];
                Shop.shopItems[3, 1] = 0;
                HealthPotCountText.text = HealthPotCount.ToString();
            }
        }
        catch (System.Exception)
        {
            return;
        }

    }

}

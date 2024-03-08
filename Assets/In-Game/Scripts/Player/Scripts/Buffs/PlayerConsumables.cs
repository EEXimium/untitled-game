using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HealthPotCountText;
    public PlayerHealth PH;
    public EffectMethods EM;
    private ShopManagerScript Shop;
       
    public int HealthPotCount;

    private void Start()
    {
        //SetHealthPotCount(HealthPotCount);
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

        if (Input.GetButtonDown("Use") && HealthPotCount > 0)
        {
            if (PH.currentHealth < PH.maxHealth)
            {
                EM.TakeHeal(2);
                HealthPotCount--;
                SetHealthPotCount(HealthPotCount);
            }
        }
        try
        {
            Shop = GameObject.Find("ShopManager").GetComponent<ShopManagerScript>();

            if (Shop.shopItems[3, 1] > 0)
            {
                HealthPotCount += Shop.shopItems[3, 1];
                Shop.shopItems[3, 1] = 0;
                SetHealthPotCount(HealthPotCount);
            }
        }
        catch (System.Exception)
        {
            return;
        }

    }
    public void SetHealthPotCount(int count)
    {
        HealthPotCountText.text = count.ToString();
    }

}

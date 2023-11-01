using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
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
    private void Update()
    {
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

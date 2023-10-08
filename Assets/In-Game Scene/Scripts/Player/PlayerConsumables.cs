using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerConsumables : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HealthPotCountText;
    public PlayerHealth PH;
    
    public int HealthPotCount = 2;

    private void Start()
    {
        HealthPotCountText.text = HealthPotCount.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && HealthPotCount > 0)
        {
            PH.TakeHeal(2);
            HealthPotCount--;
            HealthPotCountText.text = HealthPotCount.ToString();
        }
    }

}

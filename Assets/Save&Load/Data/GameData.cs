using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public float curentHealth;
    public int HealthPotCount;
    public int coinsCollected;
    //public Transform Hand1Weapon;
    //public Transform Hand2Weapon;
    

    //The values defined here must be the defaults.
    //The game starts wih this setings if there's no data o load.

    public GameData()
    {
        playerPosition = Vector3.zero;
        this.curentHealth = 20;
        HealthPotCount = 2;
        coinsCollected = 0;
        //Hand1Weapon = null;
        //Hand2Weapon = null;
    }
}

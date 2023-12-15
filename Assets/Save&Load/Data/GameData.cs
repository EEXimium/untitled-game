using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public float curentHealth;
    public int HealthPotCount;

    //The values defined here must be the defaults.
    //The game starts wih this setings if there's no data o load.

    public GameData()
    {
        playerPosition = Vector2.zero;
        this.curentHealth = 20;
        HealthPotCount = 2;
    }
}

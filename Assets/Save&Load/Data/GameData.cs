using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;

    //The values defined here must be the defaults.
    //The game starts wih this setings if there's no data o load.

    public GameData()
    {
        playerPosition = Vector2.zero;
    }
}

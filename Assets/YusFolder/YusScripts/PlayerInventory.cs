using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool hasKey = false;

    public bool HasKey
    {
        get { return hasKey; }
    }

    public void AddKey()
    {
        hasKey = true;
    }
}

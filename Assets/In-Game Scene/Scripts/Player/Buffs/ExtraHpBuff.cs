using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHpBuff : MonoBehaviour
{
    private PlayerHealth ph;
    [SerializeField] private float buffDuration = 1f;
    [SerializeField] private int extraHpGiven = 4;

    private void Start()
    {
        ph = GetComponent<PlayerHealth>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            extraHp();
            Debug.Log("T located.");
        }
    }

    void extraHp() { 
        ph.currentHealth += extraHpGiven;
        Invoke("extraHpDelete", buffDuration);
        Debug.Log("Extra hp given.");
    }
    void extraHpDelete()
    {
        ph.currentHealth -= extraHpGiven;
        Debug.Log("Extra hp deleted.");
    }
}

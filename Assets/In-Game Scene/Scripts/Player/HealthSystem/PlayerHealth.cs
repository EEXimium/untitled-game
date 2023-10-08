using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;  
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void TakeHeal(int healcount)
    {
        currentHealth += healcount;
        healthBar.SetHealth(currentHealth);
    }
}

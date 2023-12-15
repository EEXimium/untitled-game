using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour, IDataPersistence
{
    public HealthBar healthBar;
    private SpriteRenderer charSpriteR;
    [Header("Variables")]
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private Color damageColor = Color.red;

    public InsantiateText InsText;

    void Start()
    {
        currentHealth = maxHealth;  
        healthBar.SetMaxHealth(maxHealth);
        charSpriteR = GetComponent<SpriteRenderer>();
    }

    //part of the save&Load System (not working due to 21st line)
    public void LoadData(GameData data)
    {
        this.currentHealth = data.curentHealth;
    }
    public void SaveData(ref GameData data)
    {
        data.curentHealth = this.currentHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }

    private IEnumerator ColorShift()
    {
        charSpriteR.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        charSpriteR.color = Color.white;
    }

    public void TakeDamage(float damage) 
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            StartCoroutine(ColorShift());
            healthBar.SetHealth(currentHealth);
            InsText.DisplayText(this.gameObject.transform, new Vector3(0, 1, 0), Quaternion.identity, .8f, "Ughh!");

            Debug.Log("Player takes damage: " + damage);
            Debug.Log("Player takes damage. Current Health: " + currentHealth);
        }
    }

}

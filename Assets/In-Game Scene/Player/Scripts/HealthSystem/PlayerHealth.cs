using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private SpriteRenderer charSpriteR;
    [Header("Variables")]
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private Color damageColor = Color.red;

    public InsantiateText InsText;

    void Start()
    {
        SetMaxHealth(maxHealth);
        //SetHealth(currentHealth);
        charSpriteR = GetComponent<SpriteRenderer>();
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
            SetHealth(currentHealth);
            InsText.DisplayText(this.gameObject.transform, new Vector3(0, 1, 0), Quaternion.identity, .8f, "Ughh!");

            Debug.Log("Player takes damage: " + damage);
            Debug.Log("Player takes damage. Current Health: " + currentHealth);
        }
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

}

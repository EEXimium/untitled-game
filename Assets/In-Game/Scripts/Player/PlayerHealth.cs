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
    public bool isDead = false;
    public Gradient gradient;
    public Image fill;
    public SpriteRenderer eyefill;

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
        if (!isDead)
        {
            currentHealth -= damage;
            StartCoroutine(ColorShift());
            SetHealth(currentHealth);
            InsText.DisplayText(this.gameObject.transform, new Vector3(0, 1, 0), Quaternion.identity, .8f, "Ughh!");

            Debug.Log("Player takes damage: " + damage);
            Debug.Log("Player takes damage. Current Health: " + currentHealth);
        }
            
        

        if (currentHealth <= 0) { Die(); }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("�ld�n");
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        fill.color = gradient.Evaluate(1f);
        eyefill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        eyefill.color = gradient.Evaluate(slider.normalizedValue);
    }

}

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    private SpriteRenderer charSpriteR;
    [Header("Variables")]
    public float maxHealth = 4f;
    public float currentHealth;
    [SerializeField] private Color damageColor = Color.red;

    void Start()
    {
        currentHealth = maxHealth;  
        healthBar.SetMaxHealth(maxHealth);
        charSpriteR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
            StartCoroutine(ColorShift());
            DisplayText();
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
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void TakeHeal(float healcount)
    {
        currentHealth += healcount;
        healthBar.SetHealth(currentHealth);
    }


    //UsableScript!!
    public string textToDisplay = "UGhh!";
    public float deleteTime = 0.2f;

    public void DisplayText()
    {
        GameObject textObject = Instantiate(new GameObject("text"), Vector3.zero, Quaternion.identity);
        TextMesh textMesh = textObject.AddComponent<TextMesh>();
        textMesh.text = textToDisplay;
        textObject.transform.Translate(0,0,-1);
        Object.Destroy(GameObject.Find("text"));// method creates a blank object somehow this is for it.
        Object.Destroy(textObject, deleteTime);
    }
}

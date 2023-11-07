using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;

public class EffectMethods : MonoBehaviour
{
    public float fireDamage = 1f; // Damage per second
    private bool isOnFire = false;
    public bool isStuck = false; // Flag to check if the movement is frozen. Added By Yus
    private float fireDuration = 5f;
    private PlayerHealth PH;
    [SerializeField] private float FlamePeriod = 2.2f;
    [SerializeField] private float DamageCooldown = 2f;
    private PlayerMovement PM;
    public HealthBar HB;

    private float lastDamageTime = 0f;

    private void Start()
    {
       PH = GetComponent<PlayerHealth>();
       PM = GetComponent<PlayerMovement>();
        lastDamageTime = -FlamePeriod;
    }
    private void Update()
    {
        if (ExtraHpActive)
        {
            buffDurationText -= Time.deltaTime;
            intDuration = Convert.ToInt32(buffDurationText);
            timer.text = intDuration.ToString();
        }
        // Set On fire+
        if (isOnFire)
        {
            if (Time.time >= lastDamageTime)
            {
                PH.TakeDamage(fireDamage);
                lastDamageTime = Time.time + 1f / DamageCooldown;
            }

            fireDuration -= Time.deltaTime;
            if (fireDuration <= 0)
            {
                isOnFire = false;
            }
        }
        // Set on fire-
    }

    public void SetOnFire()
    {
        fireDuration = 5f;
        isOnFire = true;
    }

    public IEnumerator StuckPlayerCoroutine(float duration)
    {
        isStuck = true;
        yield return new WaitForSeconds(duration);
        isStuck = false;
    }
    public void TakeHeal(float healcount)
    {
        PH.currentHealth += healcount;
        PH.healthBar.SetHealth(PH.currentHealth);
    }


    //#########  ExtraHpBuff  #########

    [SerializeField] private float buffDuration = 1f;
    [SerializeField] private int extraHpGiven = 4;
    private float buffDurationText;

    public GameObject healtBuffIco;
    public bool ExtraHpActive;
    public TextMeshProUGUI timer;
    public int intDuration;

    public IEnumerator ExtraHp()
    {
        healtBuffIco.SetActive(true);
        PH.currentHealth += extraHpGiven;
        HB.SetHealth(PH.currentHealth);
        
        ExtraHpActive = true;
        buffDurationText = buffDuration;

        yield return new WaitForSeconds(buffDuration);
        ExtraHpActive=false;
        healtBuffIco.SetActive(false);
        PH.currentHealth -= extraHpGiven;
        HB.SetHealth(PH.currentHealth);
    }
}

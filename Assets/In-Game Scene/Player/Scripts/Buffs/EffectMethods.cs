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
    private GameObject BuffLayout;
    private TextMeshProUGUI SBtimer;
    private TextMeshProUGUI ExtHpTimer;
    private float lastDamageTime = 0f;

    private void Start()
    {
       PH = GetComponent<PlayerHealth>();
       PM = GetComponent<PlayerMovement>();

        lastDamageTime = -FlamePeriod;

        BuffLayout = GameObject.Find("Buff Layout");

        SBtimer = speedBuffPrefab.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        ExtHpTimer = healtBuffPrefab.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        // Speed Buff
        if (SBActive)
        {
            SBbuffDurationText -= Time.deltaTime;
            SBintDuration = Convert.ToInt32(SBbuffDurationText);
            SBtimer.text = SBintDuration.ToString();
        }

        // Extra Hp
        if (ExtraHpActive)
        {
            buffDurationText -= Time.deltaTime;
            intDuration = Convert.ToInt32(buffDurationText);
            ExtHpTimer.text = intDuration.ToString();
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
    [Header("Extra Hp Buff Settings")]
    [SerializeField] private float buffDuration = 1f;
    [SerializeField] private int extraHpGiven = 4;
    private float buffDurationText;

    public GameObject healtBuffPrefab;
    public bool ExtraHpActive;

    private int intDuration;

    public IEnumerator ExtraHp()
    {
        GameObject HealthBuffIcon = Instantiate(healtBuffPrefab, BuffLayout.transform);
        PH.currentHealth += extraHpGiven;
        HB.SetHealth(PH.currentHealth);
        
        ExtraHpActive = true;
        buffDurationText = buffDuration;

        yield return new WaitForSeconds(buffDuration);
        ExtraHpActive=false;
        Destroy(HealthBuffIcon);
        PH.currentHealth -= extraHpGiven;
        HB.SetHealth(PH.currentHealth);
    }

    //#########  SpeedBuff  #########
    [Header("Speed Buff Settings")]
    [SerializeField] private float SBbuffDuration = 1f;
    [SerializeField] private float SBpercentage = 0.8f;
    [SerializeField] private float SpeedGiven;
    private float SBbuffDurationText;

    public GameObject speedBuffPrefab;
    public bool SBActive;
 
    private int SBintDuration;

    public IEnumerator SpeedBuff()
    {
        GameObject SpeedBuffIcon = Instantiate(speedBuffPrefab, BuffLayout.transform);
        SpeedGiven = PM.movespeed * SBpercentage;
        PM.movespeed += SpeedGiven;

        SBActive = true;
        SBbuffDurationText = SBbuffDuration;

        yield return new WaitForSeconds(SBbuffDuration);
        SBActive = false;
        Destroy(SpeedBuffIcon);
        PM.movespeed -= SpeedGiven;
    }
}

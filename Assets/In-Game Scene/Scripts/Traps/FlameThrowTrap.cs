using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowTrap : MonoBehaviour
{
    private BoxCollider2D OutLine;
    private CapsuleCollider2D HitBox;
    private PolygonCollider2D FlameSpace;

    private Animator anim;

    private bool CanFlame;
    private bool Flaming;

    private float lastDamageTime = 0f;
    [SerializeField] private float FlamePeriod = 2.2f;
    [SerializeField] private float FlameDamage = 0.8f;
    [SerializeField] private float DamageCooldown = 2f;


    private void Start()
    {
        OutLine = GetComponent<BoxCollider2D>();
        HitBox = GetComponent<CapsuleCollider2D>();
        FlameSpace = GetComponent<PolygonCollider2D>();

        anim = GetComponent<Animator>();

        CanFlame = true;
        Flaming = false;

        lastDamageTime = -FlamePeriod;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth PH = collision.GetComponent<PlayerHealth>();
        if (PH != null)
        {
            if (CanFlame)
            {
                StartCoroutine(FlameOn());               
            }

            if (Flaming && Time.time >= lastDamageTime)
            {
                PH.TakeDamage(FlameDamage);
                lastDamageTime = Time.time + 1f / DamageCooldown;
            }
        }
    }

    private IEnumerator FlameOn()
    {
        CanFlame = false;
        anim.SetTrigger("FlameOn");
        OutLine.enabled = false;
        yield return new WaitForSeconds(FlamePeriod);
        CanFlame = true; 
    }
    public void FlameThrowActivate() 
    { 
        FlameSpace.enabled = true; 
        Flaming = true; 
    }  
    public void FlameThrowDeActivate() 
    { 
        FlameSpace.enabled = false; 
        OutLine.enabled = true; 
        Flaming = false; 
    }

}


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

    [SerializeField] private float FlamePeriod = 2.2f;
    [SerializeField] private float FlameDamage = 0.8f;


    private void Start()
    {
        OutLine = GetComponent<BoxCollider2D>();
        HitBox = GetComponent<CapsuleCollider2D>();
        FlameSpace = GetComponent<PolygonCollider2D>();

        anim = GetComponent<Animator>();

        CanFlame = true;
        Flaming = false;
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

            if (Flaming)
            {
                PH.TakeDamage(FlameDamage);
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


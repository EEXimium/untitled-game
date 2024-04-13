using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class electrizzity : Weapon, IAttack
{
    public Transform attackPoint; // Assign this in the Inspector
    public BoxCollider2D bcoll;
    public BoxCollider2D bcollright;
    public Animator anim;

    public int attack1Damage = 54;
    public int knocbackPower = 10;
    public float attack1Rate = 5f;
    private float nextAttackTime = 0f;
    public float hitRange = 0.5f;
    private int currentAttackHand = 1;
    
    public int attack2Damage = 54;
    public int knocback2Power = 10;
    public float attack2Rate = 3f;
    public float hitRange2 = 0.5f;
    public Transform firePoint1;
    public Transform firePoint2;
    public GameObject BulletPrefab;
    


    protected override void Update()
    {
        if (this.transform.parent != null)
        {
            base.Update();
            Attack();           
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Npc enemy = collision.GetComponent<Npc>();
        if (enemy != null)
        {
            enemy.TakeDamage(attack1Damage);
            enemy.ApplyKnockback(enemy.transform.position - attackPoint.position, knocbackPower);
        }

        Npc npcHealth = collision.GetComponent<Npc>();
        if (npcHealth != null)
        {
            npcHealth.TakeDamage(attack1Damage);
            npcHealth.ApplyKnockback(npcHealth.transform.position - attackPoint.position, knocbackPower);
        }
    }

    // -------------------------- Anim Activation -----------------------------
    public void ElecGloveCollTrue() 
    {
        if (currentAttackHand == 2)
        {
            bcoll.enabled = true;
        }
        else if (currentAttackHand == 1)
        {
            bcollright.enabled = true;
        }
    }
    public void ElecGloveCollFalse()
    {
        bcoll.enabled = false;
        bcollright.enabled = false;
    }
    // -------------------------------------------------------------------------

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            currentAttackHand = (currentAttackHand == 1) ? 2 : 1;
            anim.SetInteger("Counter", currentAttackHand);
            nextAttackTime = Time.time + 1f / attack1Rate;
        }

        if (Input.GetMouseButtonDown(2) && Time.time >= nextAttackTime)
        {
            currentAttackHand = (currentAttackHand == 1) ? 2 : 1;
            anim.SetInteger("Counter", currentAttackHand + 2);
            nextAttackTime = Time.time + 1f / attack2Rate;

            if (currentAttackHand == 1)
            {
                Instantiate(BulletPrefab, firePoint1.position, firePoint1.rotation);
            }
            else if (currentAttackHand == 2)
            {
                Instantiate(BulletPrefab, firePoint2.position, firePoint2.rotation);
            }
        }
    }

}

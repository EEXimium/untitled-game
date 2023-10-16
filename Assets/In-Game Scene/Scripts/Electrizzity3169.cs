using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class electrizzity : MonoBehaviour
{
    private GameObject character; // Assign this in the Inspector
    public Transform attackPoint; // Assign this in the Inspector
    public SpriteRenderer lightningGlove;
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
    

    public float orbitRadius = 1.5f;

     private void Start()
    {
        character = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (this.transform.parent != null)
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
            {
                currentAttackHand = (currentAttackHand == 1) ? 2 : 1;
                anim.SetInteger("Counter", currentAttackHand);
                nextAttackTime = Time.time + 1f / attack1Rate;
            }

            if (Input.GetMouseButtonDown(1) && Time.time >= nextAttackTime)
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

            // ----------------------- WEAPON ROTATE -----------------------------
            // Calculate the angle based on mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 directionToMouse = mousePosition - character.transform.position;
            float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            // Orbit the gun around the character
            float currentAngle = targetAngle;
            Vector3 orbitPosition = character.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

            transform.position = orbitPosition;
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            if (directionToMouse.x < 0)
                lightningGlove.flipY = true;

            else
                lightningGlove.flipY = false;

            Vector3 aimDirection = (mousePosition - character.transform.position).normalized;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, aimAngle);
            // ----------------------------------------------------------------
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null && enemy.BossAlive)
        {
            enemy.BossTakeDamage(attack1Damage);
            enemy.EnemyKnockback(enemy.transform.position - attackPoint.position, knocbackPower);
        }

        NPCHealth npcHealth = collision.GetComponent<NPCHealth>();
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



}

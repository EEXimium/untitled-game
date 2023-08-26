using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class electrizzity : MonoBehaviour
{
    public Transform character; // Assign this in the Inspector
    public Transform attackPoint; // Assign this in the Inspector
    public SpriteRenderer lightningGlove;
    public BoxCollider2D bcoll;
    public BoxCollider2D bcollright;
    public Animator anim;

    public int attack1Damage = 54;
    public int knocbackPower = 10;
    public float attackCooldown = 0.5f;
    public float hitRange = 0.5f;
    private bool canAttack = true;
    private int currentAttackHand = 1;

    public float orbitRadius = 1.5f;

    private void Update()
    {
        // Perform attack when left mouse button is clicked
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            currentAttackHand = (currentAttackHand == 1) ? 2 : 1;
            StartCoroutine(AttackCooldown());
        }

        // ----------------------- WEAPON ROTATE -----------------------------
        // Calculate the angle based on mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - character.position;
        float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Orbit the gun around the character
        float currentAngle = targetAngle;
        Vector3 orbitPosition = character.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

        transform.position = orbitPosition;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        if (directionToMouse.x < 0)
            lightningGlove.flipY = true;

        else
            lightningGlove.flipY = false;
        
        Vector3 aimDirection = (mousePosition - character.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
        // ----------------------------------------------------------------
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null && enemy.BossAlive)
        {
            enemy.BossTakeDamage(attack1Damage);
            enemy.EnemyKnockback(enemy.transform.position - attackPoint.position, knocbackPower);
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
    // -------------------------- Anim Activation -----------------------------

    public void ElecGloveCollFalse() 
    {       
        bcoll.enabled = false;
        bcollright.enabled = false;       
    }

    private IEnumerator AttackCooldown()
    {
        anim.SetInteger("Counter", currentAttackHand);
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

}

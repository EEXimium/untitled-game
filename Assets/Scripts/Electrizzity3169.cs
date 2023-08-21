using System.Collections;
using UnityEngine;

public class electrizzity : MonoBehaviour
{
    public int attackMode1Damage = 10;
    public int attackMode2Damage = 15;
    public float attackCooldown = 0.5f;
    public float hitRange = 0.5f;
    public Transform playerTransform; // Assign this in the Inspector
    public Transform attackPoint; // Assign this in the Inspector

    private bool canAttack = true;
    private int currentAttackMode = 1;

    private void Update()
    {
          // Changing attack mode when pressing the "F" key
        if (Input.GetKeyDown(KeyCode.F))
        {
            currentAttackMode = (currentAttackMode == 1) ? 2 : 1;
        }

        // Perform attack when left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Check if enough time has passed for the next attack
            if (canAttack)
            {
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;

        // Perform the attack based on the current attack mode
        int damage = (currentAttackMode == 1) ? attackMode1Damage : attackMode2Damage;

        // Check for enemies in the weapon's hit range and apply damage
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, hitRange);
        foreach (Collider2D hitCollider in hitColliders)
        {
            // Check if the hit collider has an EnemyHealth component
            EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                // Apply damage to the enemy using the BossTakeDamage method
                enemyHealth.BossTakeDamage(damage);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}

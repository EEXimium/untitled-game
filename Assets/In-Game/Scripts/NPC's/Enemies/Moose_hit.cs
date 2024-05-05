using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moose_hit : MonoBehaviour
{
    [Header("Hit Settings")]

    [SerializeField] public float knocbackPower = 10f;
    [SerializeField] public float disabletimer = 1f;
    [SerializeField] public float damage = 1f;
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if(collision.tag == "Player")
        {
            playerMovement.DisableMovementForTime(disabletimer);
            playerMovement.applyKnockback(collision.transform.position - transform.position, knocbackPower);
            playerHealth.TakeDamage(damage);
        }

    }

}

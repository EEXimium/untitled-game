using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : Weapon, IAttack
{
    private GameObject character; // Reference to the character's Transform

    // Primary Fire (Bullet)
    public GameObject BulletPrefab;
    public Transform firePoint;
    public float fireRate;
    private float nextFireTime = 0f;

    // Secondary Fire (Granade)
    public GameObject GranadePrefab;
    public SpriteRenderer gun;
    public float granadefireRate;



    protected override void Update()
    {
        if (this.transform.parent != null)
        {
            if (this.transform.parent.tag == "Hand1" || this.transform.parent.tag == "Hand2")
            {
                base.Update();
                Attack();
            }          
        }
    }

    public void shootPrimary()
    {
        if(Time.time > nextFireTime)
        {
            Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + 1f / fireRate;
        }   
    }

    public void shootSecondary()
    {
        Instantiate(GranadePrefab, firePoint.position, firePoint.rotation);
        nextFireTime = Time.time + 1f / granadefireRate;
    }

    public void rangedNpcAttack()
    {
        if (this.transform.parent.tag == "RangedNPC")
        {
            Vector3 playerPosition = character.transform.position;

            Vector3 directionToPlayer = playerPosition - this.transform.parent.transform.position;
            float targetAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            float currentAngle = targetAngle;
            Vector3 orbitPosition = this.transform.parent.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

            transform.position = orbitPosition;
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            if (directionToPlayer.x < 0) { gun.flipY = true; }
            else { gun.flipY = false; }

            Vector3 aimDirection = directionToPlayer.normalized;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            //gun.transform.rotation = Quaternion.Euler(0, 0, aimAngle);
            transform.rotation = Quaternion.Euler(0, 0, aimAngle); // Rotate the gun towards the player

            if (Time.time >= nextFireTime)
                shootPrimary();
        }
    }
    public void Attack()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            shootPrimary();

        if (Input.GetMouseButton(2) && Time.time >= nextFireTime)
            shootSecondary();
    }
}

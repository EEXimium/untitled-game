using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameObject character; // Reference to the character's Transform
    public float orbitRadius = 1.5f;

    // Primary Fire (Bullet)
    public GameObject BulletPrefab;
    public Transform firePoint;
    public float fireRate;
    private float nextFireTime = 0f;

    // Secondary Fire (Granade)
    public GameObject GranadePrefab;
    public SpriteRenderer gun;
    public float granadefireRate;


    private void Start()
    {
        character = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (this.transform.parent != null)
        {
            if(this.transform.parent.tag == "PlayerHand1" || this.transform.parent.tag == "PlayerHand2")
            {
                // Calculate the angle based on mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 directionToMouse = mousePosition - character.transform.position;
                float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

                // Orbit the gun around the character
                float currentAngle = targetAngle;
                Vector3 orbitPosition = character.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

                transform.position = orbitPosition;
                transform.rotation = Quaternion.Euler(0, 0, currentAngle);

                if (directionToMouse.x < 0) { gun.flipY = true; }
                else { gun.flipY = false; }

                Vector3 aimDirection = (mousePosition - character.transform.position).normalized;
                float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, aimAngle);

                if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
                    shootPrimary();

                if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
                    shootSecondary();
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
}

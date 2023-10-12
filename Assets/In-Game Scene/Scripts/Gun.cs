using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private GameObject character; // Reference to the character's Transform
    public float orbitRadius = 1.5f;
    public GameObject BulletPrefab;
    public GameObject GranadePrefab;
    public Transform firePoint;
    public float fireRate;
    public float granadefireRate;
    bool cangranade = true;
    bool canshoot = true;
    public SpriteRenderer gun;

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (this.transform.parent != null)
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

            if (directionToMouse.x < 0)
            {
                gun.flipY = true;
            }
            else
            {
                gun.flipY = false;
            }

            Vector3 aimDirection = (mousePosition - character.transform.position).normalized;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, aimAngle);

            if (Input.GetMouseButton(0) && canshoot)
            {
                StartCoroutine(Shoot());
            }

            if (Input.GetMouseButton(1) && cangranade)
            {
                StartCoroutine(Granade());
            }
        }

    }

    public IEnumerator Shoot()
    {
        canshoot = false;
        Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(fireRate);
        canshoot = true;
    }
    public IEnumerator Granade()
    {
        cangranade = false;
        Instantiate(GranadePrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(granadefireRate);
        cangranade = true;
    }


}

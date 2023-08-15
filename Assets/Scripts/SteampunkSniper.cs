using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteampunkSniper : MonoBehaviour
{
    public Transform character; // Reference to the character's Transform
    public float orbitRadius = 1.5f;

    public GameObject BulletPrefab;
    public Transform firePoint;
    public float fireRate;
    bool canshoot = true;
    public SpriteRenderer gun;

    private void Update()
    {
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
        {
            gun.flipY = true;
        }
        else
        {
            gun.flipY = false;
        }

        Vector3 aimDirection = (mousePosition - character.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);

        if (Input.GetMouseButton(0) && canshoot)
        {
            StartCoroutine(Shoot());
        }

    }

    public IEnumerator Shoot()
    {
        canshoot = false;
        Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(fireRate);
        canshoot = true;
    }
}

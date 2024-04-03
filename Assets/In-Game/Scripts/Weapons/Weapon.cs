using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Weapon : MonoBehaviour
{
    private GameObject character;
    public float orbitRadius = 1.5f;
    public SpriteRenderer WeaponSpr;

    protected virtual void Start()
    {
        character = GameObject.FindWithTag("Player");
    }
    

    
    protected virtual void Update()
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

        if (directionToMouse.x < 0) { WeaponSpr.flipY = true; }
        else { WeaponSpr.flipY = false; }

        Vector3 aimDirection = (mousePosition - character.transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}

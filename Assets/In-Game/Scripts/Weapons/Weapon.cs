using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Weapon : MonoBehaviour
{
    private GameObject character;
    public float orbitRadius = 1.5f;
    public SpriteRenderer WeaponSpr;
    private GameObject Cross;

    protected virtual void Start()
    {
        character = GameObject.FindWithTag("Player");
        Cross = GameObject.FindWithTag("Cross");
    }
    

    
    protected virtual void Update()
    {
        // Calculate the angle based on mouse position
        Vector3 CrossPosition = Cross.transform.position;
        Vector3 directionToCross = CrossPosition - character.transform.position;
        float targetAngle = Mathf.Atan2(directionToCross.y, directionToCross.x) * Mathf.Rad2Deg;

        // Orbit the gun around the character
        float currentAngle = targetAngle;
        Vector3 orbitPosition = character.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

        transform.position = orbitPosition;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        if (directionToCross.x < 0) { WeaponSpr.flipY = true; }
        else { WeaponSpr.flipY = false; }

        Vector3 aimDirection = (CrossPosition - character.transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}

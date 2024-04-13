using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewLighting : MonoBehaviour
{
    private GameObject character; // Reference to the character's Transform
    public float orbitRadius = 1.5f;

    private void Start()
    {
        character = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (this.transform.parent != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 directionToMouse = mousePosition - character.transform.position;
            float targetAngle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

            float currentAngle = targetAngle;
            Vector3 orbitPosition = character.transform.position + Quaternion.Euler(0, 0, currentAngle) * Vector3.right * orbitRadius;

            transform.position = orbitPosition;
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            Vector3 aimDirection = (mousePosition - character.transform.position).normalized;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, aimAngle - 90);
        }
    }
}

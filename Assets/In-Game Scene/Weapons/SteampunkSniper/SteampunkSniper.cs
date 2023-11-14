using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SteampunkSniper : MonoBehaviour
{
    private GameObject character;
    public GameObject BulletPrefab;
    public Transform firePoint;
    public SpriteRenderer gun;

    public float orbitRadius = 1.5f;  // silahýn karaktere olan uzaklýðý ya da yakýnlýðý
    public float fireRate;
    private float nextFireTime = 0f;


    // Camera Movement
    private new Camera camera;
    private GameObject camlimitrender;
    private SpriteRenderer camlimitrenderSprite;
    private float limitminX, limitmaxX, limitminY, limitmaxY;
    Vector3 StartMousePos;

    private void Start()
    {
        camera = Camera.main;
        character = GameObject.FindWithTag("Player");
        camlimitrender = GameObject.Find("CameraLimit");
        camlimitrenderSprite = camlimitrender.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (this.transform.parent != null)
        {
            // --------------- Zoom -------------------
            if (Input.GetMouseButton(1))
            {
                Onzoom();
            }
            if (Input.GetMouseButtonUp(1))
            {
                Offzoom();
            }

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

            if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
            {
                Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }      
    }

    // --------------- Zoom -------------------
    private void Onzoom()
    {
        limitminX = camlimitrenderSprite.transform.position.x - camlimitrenderSprite.bounds.size.x;
        limitmaxX = camlimitrenderSprite.transform.position.x + camlimitrenderSprite.bounds.size.x;

        limitminY = camlimitrenderSprite.transform.position.y - camlimitrenderSprite.bounds.size.y;
        limitmaxY = camlimitrenderSprite.transform.position.y + camlimitrenderSprite.bounds.size.y;


        Vector3 mouse2Position = camera.ScreenToWorldPoint(Input.mousePosition);     
        camera.transform.position = CameraLimit(Vector3.Slerp(camera.transform.position, mouse2Position, 5f * Time.deltaTime));

    }
    private void Offzoom()
    {
        Debug.Log("returning");
        Vector3 basepos = new Vector3(character.transform.position.x, character.transform.position.y, camera.transform.position.z);
        camera.transform.position = CameraLimit(basepos);

    }

    private Vector3 CameraLimit(Vector3 targetPosition)
    {
        float newX = Mathf.Clamp(targetPosition.x, limitminX, limitmaxX);
        float newY = Mathf.Clamp(targetPosition.y, limitminY, limitmaxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
    // ---------------------------------------
}

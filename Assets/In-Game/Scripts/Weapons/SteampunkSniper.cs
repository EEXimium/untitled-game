using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SteampunkSniper : Weapon, IAttack
{
    private GameObject character;
    public GameObject BulletPrefab;
    public Transform firePoint;

    public float fireRate;
    private float nextFireTime = 0f;


    // Camera Movement
    private new Camera camera;
    private GameObject camlimitrender;
    private SpriteRenderer camlimitrenderSprite;
    private float limitminX, limitmaxX, limitminY, limitmaxY;
    Vector3 StartMousePos;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
        camlimitrender = GameObject.Find("CameraLimit");
        camlimitrenderSprite = camlimitrender.GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (this.transform.parent != null)
        {
            base.Update();
            Attack();
            // --------------- Zoom -------------------
            if (Input.GetMouseButton(1))
            {
                Onzoom();
            }
            if (Input.GetMouseButtonUp(1))
            {
                Offzoom();
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
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
}

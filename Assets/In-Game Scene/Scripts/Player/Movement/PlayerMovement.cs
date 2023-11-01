using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public EffectMethods EM;

    // ----------- Move ------------
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float sprintspeed = 1f;
    private Vector2 currentPos;
    public Vector2 InputVector;
    private Vector2 newPos;
    public codex CodexScript;
    public GameObject UIDoc;
    public Animator animator;

    //private bool diabloState = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EM = GetComponent<EffectMethods>();
    }

    void Update()
    {


    }

    private void FixedUpdate()
    {
        // ----------- Move ------------
        currentPos = rb.position;
        InputVector.x = Input.GetAxisRaw("Horizontal");
        InputVector.y = Input.GetAxisRaw("Vertical");

        InputVector.Normalize(); //Diagonal hareketin bozuk hýzlý olmamasý için
        InputVector = Vector2.ClampMagnitude(InputVector, 1);    // Diagonal movement 1,4 => 1

        if (!EM.isStuck)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                newPos = currentPos + (InputVector * (movespeed + sprintspeed) * Time.fixedDeltaTime);
            }
            else
            {
                newPos = currentPos + (InputVector * movespeed * Time.fixedDeltaTime);
            }

            rb.MovePosition(newPos);
        }

    }

    /*
    public void endDiablo()
    {
        diabloState = false;
    }
    */

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weapon"))
        {
            string collectedWeaponName = other.gameObject.name; // Assuming the weapon's GameObject name matches its weapon name
            collectedWeapons.Add(collectedWeaponName);
            unlockedWeapons[collectedWeaponName] = true;

            // Optional: Deactivate the collected weapon object
            other.gameObject.SetActive(false);
        }
    }
    */

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("weapon"))
    //    {
    //        string collectedWeaponName = other.gameObject.name;
    //        Texture2D collectedWeaponSprite = other.GetComponent<SpriteRenderer>().sprite.texture;
    //        // Call the method to update the collected weapon in the codex script
    //        //CodexScript.UpdateCollectedWeapon(collectedWeaponName);
    //        UIDoc.GetComponent<codex>().CollectedWeaponUpdater(collectedWeaponName, collectedWeaponSprite);


    //        // Deactivate the collected weapon object
    //        other.gameObject.SetActive(false);
    //    }
    //}
}

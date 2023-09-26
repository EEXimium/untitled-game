using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // ----------- Move ------------
    [SerializeField] private float movespeed = 3f;
    [SerializeField] private float sprintspeed = 1f;
    private Vector2 currentPos;
    public Vector2 InputVector;
    private Vector2 newPos;
    public codex CodexScript;
    public GameObject UIDoc;
    private Animator animator;

    private bool diablo = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ----------- Move ------------
        currentPos = rb.position;
        InputVector.x = Input.GetAxisRaw("Horizontal");
        InputVector.y = Input.GetAxisRaw("Vertical");
        InputVector.Normalize(); //Diagonal hareketin bozuk h�zl� olmamas� i�in
        InputVector = Vector2.ClampMagnitude(InputVector, 1);    // Diagonal movement 1,4 => 1

        

        if (Input.GetKey(KeyCode.LeftShift))      
        { newPos = currentPos + (InputVector * (movespeed + sprintspeed) * Time.fixedDeltaTime); }

        else
        { newPos = currentPos + (InputVector * movespeed * Time.fixedDeltaTime); }

        if (Input.GetKeyDown(KeyCode.F) && !diablo)
        {
            diablo = true;
            animator.SetTrigger("FKeyPressed");
        }

        rb.MovePosition(newPos);

        animator.SetFloat("Horizontal", InputVector.x);
        animator.SetFloat("Vertical", InputVector.y);
    }

    public void endDiablo()
    {
        diablo = false;
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(newPos);
        //rb.velocity = InputVector * movespeed;
    }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("weapon"))
        {
            string collectedWeaponName = other.gameObject.name;
            Texture2D collectedWeaponSprite = other.GetComponent<SpriteRenderer>().sprite.texture;
            // Call the method to update the collected weapon in the codex script
            //CodexScript.UpdateCollectedWeapon(collectedWeaponName);
            UIDoc.GetComponent<codex>().CollectedWeaponUpdater(collectedWeaponName, collectedWeaponSprite);


            // Deactivate the collected weapon object
            other.gameObject.SetActive(false);
        }
    }
}

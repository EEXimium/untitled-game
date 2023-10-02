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
    private string currentState;

    //private bool diabloState = false;

    //ANIM STATES
    const string Idle = "Idle";
    const string Run = "Run";
    const string Walk_Left = "Walk_Left";
    const string Walk_Right = "Walk_Right";
    const string Walk_Up = "Walk_Up";
    const string Walk_Up_Left = "Walk_Up_Left";
    const string Walk_Up_Right = "Walk_Up_Right";
    const string Walk_Down = "Walk_Down";
    const string Walk_Down_Left = "Walk_Down_Left";
    const string Walk_Down_Right = "Walk_Down_Right";
    const string diabloAnim = "diablo";

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

        //------Look at cursor--------
        //Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 characterPosition = transform.position;
        //Vector2 direction = cursorPosition - characterPosition;

        //direction = Vector2.ClampMagnitude(direction, 1f);
        //direction = new Vector2(Mathf.Clamp(direction.x, -1f, 1f), Mathf.Clamp(direction.y, -1f, 1f));

        //-----------------------------------------------

        ////animator.SetFloat("Horizontal", InputVector.x);
        ////animator.SetFloat("Vertical", InputVector.y);

        //animator.SetFloat("MouseHorizontal", direction.x);
        //animator.SetFloat("MouseVertical", direction.y);
        //animator.SetFloat("Speed", InputVector.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        InputVector.Normalize(); //Diagonal hareketin bozuk hýzlý olmamasý için
        InputVector = Vector2.ClampMagnitude(InputVector, 1);    // Diagonal movement 1,4 => 1

        if (Input.GetKey(KeyCode.LeftShift))
        { 
            newPos = currentPos + (InputVector * (movespeed + sprintspeed) * Time.fixedDeltaTime); 
        }
        else
        { 
            newPos = currentPos + (InputVector * movespeed * Time.fixedDeltaTime); 
        }

        rb.MovePosition(newPos);

        //ANIM CHANGES

        if(InputVector.x >= 0.1f && InputVector.y == 0)
        {
            ChangeAnimationState(Walk_Right);
        }
        else if(InputVector.x <= -0.1f && InputVector.y == 0)
        {
            ChangeAnimationState(Walk_Left);
        }
        else if(InputVector.x == 0 && InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up);
        }
        else if (InputVector.x >= 0.1f && InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up_Right);
        }
        else if (InputVector.x <= -0.1f && InputVector.y >= 0.1)
        {
            ChangeAnimationState(Walk_Up_Left);
        }
        else if (InputVector.x == 0 && InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down);
        }
        else if (InputVector.x >= 0.1f && InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down_Right);
        }
        else if (InputVector.x <= -0.1f && InputVector.y <= -0.1)
        {
            ChangeAnimationState(Walk_Down_Left);
        }
        else
        {
            ChangeAnimationState(Idle);
        }

        /*
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeAnimationState(diabloAnim);
        }
        */

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

    void ChangeAnimationState(string newState)
    {
        //halihazýrda oynayan animasyonun kendini kesmesine engel ol
        if (currentState == newState)
            return;

        //yeni anim oynat
        animator.Play(newState);

        //current state güncelle çünkü deðiþti
        currentState = newState;
    }
}

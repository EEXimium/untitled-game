using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // ----------- Move ------------
    [SerializeField] private float movespeed = 2f;
    private Vector2 currentPos;
    private Vector2 InputVector;
    private Vector2 newPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        // ----------- Move ------------
        currentPos = rb.position;     
        InputVector.x = Input.GetAxisRaw("Horizontal");     
        InputVector.y = Input.GetAxisRaw("Vertical");
        InputVector = Vector2.ClampMagnitude(InputVector, 1);    // Diagonal movement 1,4 => 1
        newPos = currentPos + (InputVector * movespeed * Time.fixedDeltaTime);   
    }

    private void FixedUpdate()
    {
        // ----------- Move ------------
        rb.MovePosition(newPos);
    }
}

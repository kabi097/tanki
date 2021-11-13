using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement // Inherits from the Movement class
{
    float h, v; // Variables for the movement inputs
    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Gets current object rigidbody2d element
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Calls the moving functions if axis inputs are detected and object isn't currently moving
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h, rb2d)); 
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v, rb2d));
    }
}

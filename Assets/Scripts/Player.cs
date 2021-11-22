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

        FindObjectOfType<AudioManager>().Play("NotMoving"); //Plays not moving sfx
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
        if (h != 0 && !isMoving)
        {
            StartCoroutine(MoveHorizontal(h, rb2d));

            if(!FindObjectOfType<AudioManager>().IsPlaying("Moving")) //Checks if the sound is not already playing
            {
                FindObjectOfType<AudioManager>().Play("Moving"); //Plays moving sfx
                FindObjectOfType<AudioManager>().Stop("NotMoving"); //Stops standing still sfx
            }
        }
        else if (v != 0 && !isMoving)
        {
            StartCoroutine(MoveVertical(v, rb2d));

            if (!FindObjectOfType<AudioManager>().IsPlaying("Moving")) //Checks if the sound is not already playing
            {
                FindObjectOfType<AudioManager>().Play("Moving"); //Plays moving sfx
                FindObjectOfType<AudioManager>().Stop("NotMoving"); //Stops standing still sfx
            }
        }
        else if(!isMoving && !FindObjectOfType<AudioManager>().IsPlaying("NotMoving"))
        {
            FindObjectOfType<AudioManager>().Stop("Moving"); //Stops moving sfx
            FindObjectOfType<AudioManager>().Play("NotMoving"); //Plays not moving sfx
        }
    }
}

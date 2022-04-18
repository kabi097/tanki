using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank, IKillable, IDamageble // Inherits from the Movement class
{
    float h, v; // Variables for the movement inputs
    Rigidbody2D rb2d;

    public Canvas canvas;

    public MasterTracker masterTracker;

    public int speed = 5;
    protected bool isMoving = false; // Flag to ensure that the movement before has stopped and new one can be started

    public float fireTimer = 4.0f;


    int powerLevel = 0;

    private bool canFire = true;
    
    void Start()
    {
        masterTracker = MasterTracker.FindObjectOfType<MasterTracker>();
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
        health = masterTracker.GetHp();
        masterTracker.SetHpMax(maxHealth);
        powerLevel = masterTracker.GetPower();
        rb2d = GetComponent<Rigidbody2D>(); // Gets current object rigidbody2d element
        //FindObjectOfType<AudioManager>().Play("NotMoving"); //Plays not moving sfx
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        masterTracker.SetHp(health);
        masterTracker.SetPower(powerLevel);
        if (!alreadyDead)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

            if (Input.GetButton("Fire1") && GPmanager.can_move)
            {
                if (canFire)
                {
                    FindObjectOfType<AudioManager>().Play("Shot"); //Plays moving sfx
                    Shoot();
                    canFire = false;
                    if(powerLevel == 3)
                    {
                        StartCoroutine(SecondShoot());
                    }
                    StartCoroutine(FireEnable());

                }
            }
        }
    }

    void FixedUpdate()
    {
        if (GPmanager.can_move && !alreadyDead)
        {
            //Calls the moving functions if axis inputs are detected and object isn't currently moving
            if (h != 0 && !isMoving)
            {
                StartCoroutine(MoveHorizontal(h, rb2d));

                if (!FindObjectOfType<AudioManager>().IsPlaying("Moving")) //Checks if the sound is not already playing
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
            else if (!isMoving && !FindObjectOfType<AudioManager>().IsPlaying("NotMoving"))
            {
                FindObjectOfType<AudioManager>().Stop("Moving"); //Stops moving sfx
                FindObjectOfType<AudioManager>().Play("NotMoving"); //Plays not moving sfx
            }
        }
    }

    protected IEnumerator MoveHorizontal (float movementHorizontal, Rigidbody2D rb2d) // Movement functions in coroutines to make the movement smooth
    {
        isMoving = true;

        //transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Quaternion rotation = Quaternion.Euler(0, 0, -movementHorizontal * 90f); // Movement's rotation
        //body.transform.rotation = rotation ;
        transform.rotation = rotation;

        canvas.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        canvas.transform.rotation = Quaternion.Euler(0, 0, 0);


        //canvas.transform.rotation = Quaternion.Euler(0, 0, +movementHorizontal * 90f);
        float movementProgress = 0f;  // Progress of one movement (clamped later to (0.0, 1.0)
        Vector2 movement, endPos;

        while (movementProgress < 0.1f*Mathf.Abs(movementHorizontal))
        {
            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f); 
            movement = new Vector2(speed * Time.deltaTime * movementHorizontal, 0f);
            endPos = rb2d.position + movement;

            //if (movementProgress == 1) endPos = new Vector2(Mathf.Round(endPos.x), endPos.y);
            rb2d.MovePosition(endPos); // Moving the object

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;

    }
    protected IEnumerator MoveVertical(float movementVertical, Rigidbody2D rb2d)
    {
        isMoving = true;

        //transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        Quaternion rotation; // Movement's rotation

        if (movementVertical < 0)
        {
            rotation = Quaternion.Euler(0, 0, movementVertical * 180f);
        }
        else
        {
            rotation = Quaternion.Euler(0, 0, 0);
        }
        transform.rotation = rotation;
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        canvas.transform.rotation = Quaternion.Euler(0, 0, 0);

        //body.transform.rotation = rotation;

        float movementProgress = 0f; // Progress of one movement (clamped later to (0.0, 1.0)
        Vector2 endPos, movement;

        while (movementProgress < 0.1f*Mathf.Abs(movementVertical))
        {

            movementProgress += speed * Time.deltaTime;
            movementProgress = Mathf.Clamp(movementProgress, 0f, 1f);

            movement = new Vector2(0f, speed * Time.deltaTime * movementVertical);
            endPos = rb2d.position + movement;

            //if (movementProgress == 1) endPos = new Vector2(endPos.x, Mathf.Round(endPos.y));
            rb2d.MovePosition(endPos); // moving the object
            yield return new WaitForFixedUpdate();

        }

        isMoving = false;
    }
    public void PowerUp()
    {
        FindObjectOfType<AudioManager>().Play("HpGot");
        if (powerLevel < 3)
        {
            //fireTimer -= 0.25f;
            powerLevel++;
        }
        else
        {
            powerLevel = 3;
        }
    }

    public bool isAlreadyDead()
    {
        return alreadyDead;
    }
    IEnumerator FireEnable()
    {
        yield return new WaitForSeconds(fireTimer-powerLevel*0.15f);
        canFire = true;
    }

    IEnumerator SecondShoot()
    {
        yield return new WaitForSeconds(0.075f);
        FindObjectOfType<AudioManager>().Play("Shot"); //Plays moving sfx
        Shoot();
    }
}

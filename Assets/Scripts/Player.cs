using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Movement, IKillable, IDamageble // Inherits from the Movement class
{
    float h, v; // Variables for the movement inputs
    Rigidbody2D rb2d;

    public Transform firePoint;
    public GameObject bulletPrefab;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // Gets current object rigidbody2d element
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    public void Damage(int damageTaken)
    {
        Debug.Log(damageTaken);
    }

    public void Kill() 
    {
        Debug.Log("killed");
    }

    void FixedUpdate()
    {
        //Calls the moving functions if axis inputs are detected and object isn't currently moving
        if (h != 0 && !isMoving) StartCoroutine(MoveHorizontal(h, rb2d)); 
        else if (v != 0 && !isMoving) StartCoroutine(MoveVertical(v, rb2d));
    }

    void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed = 20f;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) 
    {
        IDamageble damageble = hitInfo.GetComponent<IDamageble>();

        if (damageble != null) {
            damageble.Damage(-50);
        }

        Destroy(gameObject);
    }
}

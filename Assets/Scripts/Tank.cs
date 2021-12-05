using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IKillable, IDamageble
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int health = 200;

    

    public GameObject deathEffect;

    public void Damage(int damage) 
    {
        health += damage;

        if (health <= 0) {
            Kill();
        }
    }

    public void Kill() 
    {
        if (deathEffect != null) {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    public void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

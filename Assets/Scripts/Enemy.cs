using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IKillable, IDamageble
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int health = 200;

    float elapsed = 0f;

    // public GameObject deathEffect;
    private void Update() 
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            Shoot();
        }
    }

    public void Damage(int damage) 
    {
        health += damage;

        if (health <= 0) {
            Kill();
        }
    }

    public void Kill() 
    {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
    void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}

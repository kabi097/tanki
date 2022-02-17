using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour, IKillable, IDamageble
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int health = 200;

    public CameraShake Cshake;

    public GamePlayManager GPmanager;
    

    public GameObject deathEffect;

    

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
    public void Damage(int damage)
    {
        if (gameObject.name == "Player")
        {
            StartCoroutine(Cshake.Shake(0.05f, 0.3f));
        }
        else
        {
            StartCoroutine(Cshake.Shake(0.015f, 0.15f));
        }

        health += damage;
        if (health <= 0)
        {
            Kill();
        }
    }
}

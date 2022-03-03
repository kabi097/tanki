using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    float elapsed = 0f;


    public GameObject bulletPrefabEnemy;
    public float shootingSpeed = 2f;
    public float shootingRangeSpeed = 0.5f;


    private void Start()
    {
        healthBar.SetMaxHealth(health);
    }
    public bool IsAlreadyDead()
    {
        return alreadyDead;
    }
    // public GameObject deathEffect;
    private void Update() 
    {
        elapsed += Time.deltaTime;
        if (elapsed >= (shootingSpeed + Random.Range(-shootingRangeSpeed, shootingRangeSpeed)) && GPmanager.can_move && !alreadyDead) {
            elapsed -= shootingSpeed;
            EnemyShoot();
        }
    }


    public void EnemyShoot()
    {
        Instantiate(bulletPrefabEnemy, firePoint.position, firePoint.rotation);
    }
}

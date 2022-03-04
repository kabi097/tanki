using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    float elapsed = 0f;


    public GameObject bulletPrefabEnemy;
    public float shootingSpeed = 2f;
    public float shootingRangeSpeed = 0.5f;
    public bool doubleShotEnable = false;
    bool shooted = false;


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
        if(elapsed >= 0.1f && shooted && doubleShotEnable)
        {
            if(Random.value > 0.5f) EnemyShoot();
            shooted = false;
        }
        if (elapsed >= (shootingSpeed + Random.Range(-shootingRangeSpeed, shootingRangeSpeed)) && GPmanager.can_move && !alreadyDead) {
            elapsed = 0;
            shooted = true;
            EnemyShoot();
        }
    }


    public void EnemyShoot()
    {
        Instantiate(bulletPrefabEnemy, firePoint.position, firePoint.rotation);
    }
}

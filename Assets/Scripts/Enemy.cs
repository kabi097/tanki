using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
    float elapsed = 0f;

    public GameObject bulletPrefabEnemy;

    // public GameObject deathEffect;
    private void Update() 
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f && GPmanager.can_move) {
            elapsed = elapsed % 1f;
            EnemyShoot();
        }
    }


    public void EnemyShoot()
    {
        Instantiate(bulletPrefabEnemy, firePoint.position, firePoint.rotation);
    }
}

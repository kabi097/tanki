using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Tank
{
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
}

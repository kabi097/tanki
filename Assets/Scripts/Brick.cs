using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, IKillable, IDamageble // Inherits from the Movement class
{
    public void Damage(int damageTaken) 
    {
        Kill();
    }

    public void Kill() 
    {
        Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tank : MonoBehaviour, IKillable, IDamageble
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int health = 200;

    public SpriteRenderer sprite;

    public CameraShake Cshake;

    public GamePlayManager GPmanager;

    public ParticleSystem ExplosionParticles;

    protected bool alreadyDead = false;

    public void Kill() 
    {
        Destroy(gameObject);
    }
    
    public void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    public void Damage(int damage)
    {
        if (!alreadyDead)
        {
            if (gameObject.name == "Player")
            {
                StartCoroutine(Cshake.Shake(0.05f, 0.3f));
                FindObjectOfType<AudioManager>().Play("BigHit"); //Plays moving sfx
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("LowHit"); //Plays moving sfx
            }

            health += damage;
            if (health <= 0)
            {
                alreadyDead = true;
                FindObjectOfType<AudioManager>().Play("Destroyed"); //Plays moving sfx
                ExplosionParticles.Play();
                StartCoroutine(FadeOut());
                StartCoroutine(WaitForEffects());

            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            sprite.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    IEnumerator WaitForEffects()
    {
        while(ExplosionParticles.isPlaying) yield return null;
        Kill();
    }
}

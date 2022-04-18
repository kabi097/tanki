using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Tank : MonoBehaviour, IKillable, IDamageble
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public GameObject body;

    public GameObject HpRegen;
    public GameObject PowerRiser;


    public int health = 200;
    public int maxHealth;

    public SpriteRenderer sprite;

    public HealthBarScript healthBar;

    public CameraShake Cshake;

    public GamePlayManager GPmanager;

    public ParticleSystem ExplosionParticles;

    protected bool alreadyDead = false;




    public void Kill() 
    {
        if (gameObject.name != "Player") Destroy(gameObject);
    }
    
    public void Shoot() 
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    public void Damage(int damage)
    {
        if (!alreadyDead)
        {

            healthBar.showBar(2);

            if (gameObject.name == "Player")
            {
                if (damage < 0)
                {
                    StartCoroutine(Cshake.Shake(0.05f, 0.3f));
                    FindObjectOfType<AudioManager>().Play("BigHit"); //Plays moving sfx
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("HpGot");
                }
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("LowHit"); //Plays moving sfx
            }

            health += damage;
            healthBar.SetHealth(health);

            if (health <= 0)
            {
                alreadyDead = true;
                FindObjectOfType<AudioManager>().Play("Destroyed"); //Plays moving sfx
                ExplosionParticles.Play();
                
                StartCoroutine(FadeOut());

                
                if (gameObject.name != "Player" && Random.value > 0.70f) //spawn item when ded
                {
                    if(Random.value > 0.75f) Instantiate(PowerRiser, transform.position, Quaternion.Euler(0, 0, 0));
                    else Instantiate(HpRegen, transform.position, Quaternion.Euler(0, 0, 0));
                }
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

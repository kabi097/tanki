using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public SpriteRenderer sprite;

    bool blinkState = false;
    bool canBlink = true;


    private void Start()
    {
        sprite.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if (canBlink)
        {
            if (blinkState)
            {
                canBlink = false;
                blinkState = false;
                StartCoroutine(FadeOut());
            }
            else
            {
                canBlink = false;
                blinkState = true;
                StartCoroutine(FadeIn());
            }
        }

    }
    void OnCollisionEnter2D(Collision2D hitInfo)
    {
        if (hitInfo.gameObject.GetComponent<IDamageble>() != null)
        {
            Player player = hitInfo.gameObject.GetComponent<Player>();

            if (hitInfo.gameObject.name == "Player")
            {
                player.PowerUp();

                Destroy(gameObject);
            }
        }
    }
    IEnumerator FadeOut()
    {
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            sprite.color = new Color(1, 1, 1, Mathf.Clamp(i,0.0f,1.0f));
            yield return null;
            

        }
        canBlink = true;
    }
    IEnumerator FadeIn()
    {
        for (float i = 0; i <= 2; i += Time.deltaTime)
        {
            // set color with i as alpha
            sprite.color = new Color(1, 1, 1, Mathf.Clamp(i, 0.0f, 1.0f));
            yield return null;
            
        }
        canBlink = true;
    }
}

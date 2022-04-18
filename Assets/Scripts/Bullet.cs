using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public bool destroySteel = false;

    public Rigidbody2D rb;

    public ParticleSystem ExplosionParticles;

    public ParticleSystem TilesParticles;

    [SerializeField]
    public float speed = 20f;



    GameObject brickGameObject, steelGameObject;
    Tilemap tilemap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        brickGameObject = GameObject.FindGameObjectWithTag("Brick");
        steelGameObject = GameObject.FindGameObjectWithTag("Steel");
    }




    void OnCollisionEnter2D(Collision2D hitInfo) 
    {
        rb.velocity = Vector2.zero;

        tilemap = hitInfo.gameObject.GetComponent<Tilemap>();

        if ((hitInfo.gameObject == brickGameObject) || (destroySteel && hitInfo.gameObject == steelGameObject))
        {
            FindObjectOfType<AudioManager>().Play("BlockSmash"); //Plays moving sfx
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in hitInfo.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

                Instantiate(TilesParticles, tilemap.GetCellCenterWorld(tilemap.WorldToCell(hitPosition)), Quaternion.Euler(0, 0, 0));                     
            }
            Destroy(gameObject);

        }
        else if (hitInfo.gameObject == steelGameObject)
        {
           FindObjectOfType<AudioManager>().Play("WallHit"); //Plays moving sfx
           Destroy(gameObject);
        }

        else if (hitInfo.gameObject.GetComponent<IDamageble>() != null)
        {
            IDamageble damageble = hitInfo.gameObject.GetComponent<IDamageble>();

            if (damageble != null)
            {
                damageble.Damage(-50);

            }
            Destroy(gameObject);
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("WallHit"); //Plays moving sfx
        }
        Instantiate(ExplosionParticles, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    public bool destroySteel = false;

    public Rigidbody2D rb;
    

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
        Debug.Log("hit" + hitInfo.gameObject.name);
        rb.velocity = Vector2.zero;

        tilemap = hitInfo.gameObject.GetComponent<Tilemap>();

        Debug.Log(hitInfo.gameObject.tag);
        
        if (hitInfo.gameObject.GetComponent<IDamageble>() != null)
        {
            IDamageble damageble = hitInfo.gameObject.GetComponent<IDamageble>();

            if (damageble != null)
            {
                damageble.Damage(-50);

            }
            Destroy(gameObject);
        }
        if ((hitInfo.gameObject == brickGameObject) || (destroySteel && hitInfo.gameObject == steelGameObject))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in hitInfo.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
           
        }
        Destroy(gameObject);

    }
  
}

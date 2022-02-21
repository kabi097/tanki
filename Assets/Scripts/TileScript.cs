using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileScript : MonoBehaviour
{

    public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Bricks").GetComponent<Tilemap>();
        tilemap.SetTileFlags(tilemap.WorldToCell(gameObject.transform.position), TileFlags.None);
        StartCoroutine(FadeOut(tilemap.WorldToCell(gameObject.transform.position)));
    }

    IEnumerator FadeOut(Vector3 _pos)
    {
        for (float i = 1f; i >= 0; i -= Time.deltaTime*2)
        {
            // set color with i as alpha

            tilemap.SetColor(tilemap.WorldToCell(_pos), new Color(1, 1, 1, i));
            yield return null;
            
        }
        tilemap.SetTile(tilemap.WorldToCell(_pos), null);
    }

}

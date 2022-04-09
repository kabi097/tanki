using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 5f;
    public bool titleScreen = false;

    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (GameObject.FindWithTag("Player") != null)
        {
            Vector3 desiredPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime) ;
            transform.position = smoothedPos;
        }
        else if(titleScreen)
        {
            time += Time.deltaTime/2;
            Vector3 desiredPos = new Vector3(Mathf.Sin(time)*1.1f + 2, Mathf.Cos(time+Mathf.PI)*1.2f +1, transform.position.z);
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPos;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TSManager : MonoBehaviour
{

    [SerializeField]
    Text playText;
    [SerializeField]
    Text quitText;
    [SerializeField]
    Text creditsText;


    int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("LevelIntro"); //Plays level intro
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown("down") && state < 1)
        {
            state++;
            FindObjectOfType<AudioManager>().Play("Shot");
        }
        else if(Input.GetKeyDown("up") && state > 0)
        {
            state--;
            FindObjectOfType<AudioManager>().Play("Shot");
        }
        
        if(state == 0)
        {
            playText.fontSize = 45;
            quitText.fontSize = 35;
            //creditsText.fontSize = 35;
        }
        else if (state == 1)
        {
            playText.fontSize = 35;
            quitText.fontSize = 45;
            //creditsText.fontSize = 35;
        }
        else if (state == 2)
        {
            playText.fontSize = 35;
            quitText.fontSize = 35;
            //creditsText.fontSize = 45;
        }
        if(Input.GetButton("Fire1"))
        {
            if(state == 0)
            {
                SceneManager.LoadScene("Scenes/Level 01");
            }
            else if (state == 1)
            {
                Application.Quit();
            }
            else if (state == 2)
            {
                //credits scene
            }
        }    

    }
}

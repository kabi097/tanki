using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton MasterTracker that stores the essential informations throught the entire game
public class MasterTracker : MonoBehaviour
{
    static MasterTracker instance = null; // static variable in which MasterTracker will be stored

    public HealthBarScript UIhealth;



    [SerializeField]
    int smallTankPoints = 100, fastTankPoints = 200, bigTankPoints = 300; // how much points you get per kill per enemy tank
    // public get variable functions
    public int smallTankPointsWorth { get { return smallTankPoints; } }
    public int fastTankPointsWorth { get { return fastTankPoints; } }
    public int bigTankPointsWorth { get { return bigTankPoints; } }

    public static int smallTankDestroyed, fastTankDestroyed, bigTankDestroyed;
    public static int stageNumber;          //amount of destroyed enemy tanks, current stage number and player score
    public static int playerScore = 0;

    public static bool stageCleared = false;
    public int playerHpLeft = 200;
    public int playerPowerLevel = 0;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject); //prevents MasterTracker GameObject from being destroyed when loading a new scene
            instance = this; //storing gameObject in static variable
        }
        else if(instance != this)
        {
            Destroy(gameObject); //destroy duplicate instances
        }


    }

    private void Start()
    {
        //FindObjectOfType<AudioManager>().Play("LevelIntro"); //Plays level intro
        SetHpMax(playerHpLeft);

    }
    public void SetHpMax(int hp)
    {
        UIhealth.SetMaxHealth(hp);
    }

    public void SetHp(int hp)
    {
        playerHpLeft = hp;
        UIhealth.SetHealth(hp);
    }
    public int GetHp()
    {
        return playerHpLeft;
    }
    public int GetPower()
    {
        return playerPowerLevel;
    }
    public void SetPower(int power)
    {
        playerPowerLevel = power;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "GameClear")
        {
            //Debug.Log("err");
            Destroy(gameObject);
            
        }
    }

}

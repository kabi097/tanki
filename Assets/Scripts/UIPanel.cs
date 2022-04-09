using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPanel : MonoBehaviour
{
    [SerializeField]
    Text stageNumber;
    [SerializeField]
    Text enemyLeftNumber;

    [SerializeField]
    Text powerValueText;

    public MasterTracker mtracker;



   

    // Update is called once per frame
    void Update()
    {
        stageNumber.text = SceneManager.GetActiveScene().name;
        int enemyLeft;
        enemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyLeftNumber.text = enemyLeft.ToString();

        if (mtracker.GetPower() == 3) powerValueText.text = "MAX";
        else powerValueText.text = mtracker.GetPower().ToString();

    }
}

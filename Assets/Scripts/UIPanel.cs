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



    // Start is called before the first frame update
    void Start()
    {
         stageNumber.text = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        int enemyLeft;
        enemyLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyLeftNumber.text = enemyLeft.ToString();
    }
}

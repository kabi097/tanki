using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Image topCurtain, bottomCurtain, blackCurtain;
    [SerializeField]
    Text stageNumberText, gameOverText;
    [SerializeField]
    RectTransform canvas;

    bool tankReserveEmpty = false;

    public bool can_move = false;

    bool waitForDone = false;

    // Start is called before the first frame update
    void Start()
    {
        stageNumberText.text = SceneManager.GetActiveScene().name;
        StartCoroutine(StartStage());
    }

    private void Update()
    {
        if (GameObject.FindWithTag("Enemy") == null)
        {
            MasterTracker.stageCleared = true;
            StartCoroutine(WaitFor(2.0f));
            if (waitForDone)
            {
                waitForDone = false;
                FindObjectOfType<AudioManager>().Stop("NotMoving");
                FindObjectOfType<AudioManager>().Stop("Moving");
                LevelCompleted();
            }
        }
        if (GameObject.FindWithTag("Player") == null)
        {
            FindObjectOfType<AudioManager>().Stop("NotMoving");
            FindObjectOfType<AudioManager>().Stop("Moving");
            StartCoroutine(GameOver());
        }
    }
    private void LevelCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(blackCurtain.rectTransform.localScale.x, Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), blackCurtain.rectTransform.localScale.z);
            yield return null;
        }
    }
    IEnumerator RevealTopStage()
    {
        float moveTopUpMin = topCurtain.rectTransform.position.y + (canvas.rect.height / 2) + 10;
        stageNumberText.enabled = false;
        while (topCurtain.rectTransform.position.y < moveTopUpMin)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }
    IEnumerator RevealBottomStage()
    {
        float moveBottomDownMin = bottomCurtain.rectTransform.position.y - (canvas.rect.height / 2) - 10;
        while (bottomCurtain.rectTransform.position.y > moveBottomDownMin)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
        can_move = true;
    }
    IEnumerator StartStage()
    {
        can_move = false;
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        

    }
    public IEnumerator GameOver()
    {
        while (gameOverText.rectTransform.localPosition.y < 0)
        {
            gameOverText.rectTransform.localPosition = new Vector3(gameOverText.rectTransform.localPosition.x, gameOverText.rectTransform.localPosition.y + 1f * Time.deltaTime, gameOverText.rectTransform.localPosition.z);
            yield return null;
        }
        MasterTracker.stageCleared = false;
        //SceneManager.LoadScene("Level 00"); // return to title screen
    }
    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);
        waitForDone = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject endScreen;
    public Image gameOverScreen;
    public Sprite win;
    public Sprite lose;
    public bool conditionMet;
    public bool coroutineStarted;
    public bool wonGame;
    public bool lostGame;
    public CoreData coreRef;
    void Start()
    {
        gameOverScreen = endScreen.GetComponent<Image>();
        if(Time.timeScale <= 0)
        {
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectWinConditions();
        if(conditionMet && !coroutineStarted)
        {
            StartCoroutine(EndGame());
            coroutineStarted = true;
        }
       // print(Time.timeScale);
    }

    public IEnumerator EndGame()
    {
        endScreen.SetActive(true);
        if(wonGame)
        {
            gameOverScreen.sprite = win;
        }
        else
        {
            gameOverScreen.sprite = lose;
        }

        yield return null;
    }

    public void DetectWinConditions()
    {
        if(coreRef.getEnergy() >= coreRef.getMaxEnergy())
        {
            conditionMet = true;
            wonGame = true;
        }
        else if(coreRef.getEnergy() <= 0)
        {
            conditionMet = true;
            lostGame = true;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isOver;
    public bool isTimesOut;
    public GameObject gameOverPanel;
    public TextMeshProUGUI coinResultText;

    [Header("Timer Countdown Attribute")]
    public TextMeshProUGUI timerText;
    public float timer;

    [Header("Point And Coin Attribute")]
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI coinText;
    public int point, coin;

    void Start()
    {
        coin = PlayerPrefs.GetInt("Coins");    
    }

    // Update is called once per frame
    void Update()
    {
        //update point and coin text
        pointText.text = $"{point}";
        coinText.text = $"{coin}";

        //play countdown
        if (!isTimesOut)
        {
            if (timer > 0)
                UpdateTimer(timer);
            else
                isTimesOut = true;
        }

        //check if player win
        if (isTimesOut && !isOver || 
            point >= 1000 && !isOver)
        {
            if (point >= 1000)
                coin += (int)timer;
            
            gameOverPanel.SetActive(true);
            coinResultText.text = $"{coin}";
            PlayerPrefs.SetInt("Coins", coin);

            isOver = true;
            timer = 0;
        }
    }

    void UpdateTimer(float currentTime)
    {
        timer -= Time.deltaTime;
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
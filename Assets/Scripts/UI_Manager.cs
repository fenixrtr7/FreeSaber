using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager sharedInstance;
    [Header("Score")]
    public Text scoreText;
    int scoreGame = 0;
    [Header("Time")]
    public Text timeText;
    float timeGame = 120;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + scoreGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            timeText.text = "Time: " + timeGame.ToString("0.0");

            if (timeGame <= 0)
            {
                timeGame = 0;

                GameManager.sharedInstance.GameOver();
            }
            else
            {
                timeGame -= Time.deltaTime;
            }
        }
    }

    public void AddPoint(int pointsToAdd)
    {
        scoreGame += pointsToAdd;
        scoreText.text = "Score: " + scoreGame;
    }
}

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
    public GameObject cuboIndicadorWin;
    public Material materialACambiarGood, materialBad;
    public Material materialActual;
    Material actualMaterial;
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
        if(pointsToAdd > 0)
        {
            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialACambiarGood;

            Debug.Log("Funciona");
        }
        else if(pointsToAdd < 0)
        {
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialBad;
            Debug.Log("Funciona x2");
        }
        scoreGame += pointsToAdd;
        scoreText.text = "Score: " + scoreGame;
        StartCoroutine(BackMaterial());
    }

    IEnumerator BackMaterial()
    {
        yield return new WaitForSeconds(0.2f);

        cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialActual;
    }
}

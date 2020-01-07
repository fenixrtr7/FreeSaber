using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager sharedInstance;
    [Header("Score")]
    public Text scoreText, multipliText;
    float multipliNumber = 1;

    [Header("Material")]
    public GameObject cuboIndicadorWin;
    public Material materialACambiarGood, materialBad, meterialVeryGood;
    public Material materialActual;

    [Header("Time")]
    public Text timeText, regresiveCountText;
    float counterTime = 3;
    bool counterStart = false;

    [Header("Particle")]
    public ParticleSystem particleObj;

    [Header("Prefab")]
    // Instanciar numeros puntos
    public GameObject instantiatePoint;
    public GameObject numberPrefab;

    // Fade
    public FadeImage fadeImage;

    // Music
    SelectMusic selectMusic;

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
        //Debug.Log("Counter: "  + counterTime);

        selectMusic = FindObjectOfType<SelectMusic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        counterTime = 3;

        counterStart = true;

        scoreText.text = "Score: " + GamePreparationManager.currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            timeText.text = Epoch.GetTimerString((int)GamePreparationManager.timeGame);

            if (GamePreparationManager.timeGame <= 0)
            {
                GamePreparationManager.timeGame = 0;
                // Pause Music
                selectMusic.PauseMusic();

                GameManager.sharedInstance.GameOver();
            }
            else if (GamePreparationManager.timeGame <= 10)
            {
                timeText.color = Color.red;
                GamePreparationManager.timeGame -= Time.deltaTime;
            }
            else
            {
                GamePreparationManager.timeGame -= Time.deltaTime;
            }
        }
        else if (GameManager.sharedInstance.currentGameState == GameState.menu)
        {
            regresiveCountText.text = counterTime.ToString("0");

            if (counterTime <= 0 && counterStart)
            {
                counterStart = false;

                Debug.Log("Counter: "  + counterTime);
                counterTime = 0;

                GameManager.sharedInstance.StarGame();
            }
            else
            {
                counterTime -= Time.deltaTime;
            }
        }
    }

    public void AddPoint(long pointsToAdd)
    {
        if (pointsToAdd > 0 && pointsToAdd < 30)
        {
            // Reproducioms particulas
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer>().material = materialACambiarGood;
            //Debug.Log("Funciona");
            RestetMultipli();
        }
        else if (pointsToAdd < 0)
        {
            // Fade
            fadeImage.FadeImageObj();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer>().material = materialBad;
            //Debug.Log("Funciona x2");
            RestetMultipli();
        }
        else if (pointsToAdd > 30)
        {
            // Reproducioms particulas
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer>().material = meterialVeryGood;
            //Debug.Log("Funciona");
        }
        // Se multiplica el Score
        GamePreparationManager.currentScore *= (long)multipliNumber;

        // Mostramos en pantalla puntos
        ShowNumber(pointsToAdd);

        GamePreparationManager.currentScore += pointsToAdd;
        //Debug.Log("Score: " + scoreGame);

        // Score no < 0
        if (GamePreparationManager.currentScore <= 0)
        {
            GamePreparationManager.currentScore = 0;
        }

        scoreText.text = "Score: " + GamePreparationManager.currentScore;
        StartCoroutine(BackMaterial());
    }

    //Cambiamos materiasl
    IEnumerator BackMaterial()
    {
        yield return new WaitForSeconds(0.2f);

        cuboIndicadorWin.GetComponent<MeshRenderer>().material = materialActual;
    }

    // Mostramos en pantalla puntos
    void ShowNumber(float pointsShow)
    {
        var clone = (GameObject)Instantiate(numberPrefab, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
        clone.GetComponent<DamageNumber>().damagePoints = pointsShow;
    }

    // Suma al multiplicador
    public void AddMultiPoints()
    {
        multipliNumber += 0.2f;
        multipliText.text = "x" + multipliNumber;
        Debug.Log("Multiplicador: " + multipliNumber);
    }

    // Reset para pultiplicador
    public void RestetMultipli()
    {
        multipliNumber = 1;
        multipliText.text = "x" + multipliNumber;
    }

    public void ButtonRestart()
    {
        GameManager.sharedInstance.RestartGame();
    }
}

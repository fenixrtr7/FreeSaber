using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager sharedInstance;
    [Header("Score")]
    public Text scoreText, multipliText;
    float scoreGame = 0;
    float multipliNumber = 1;

    [Header("Material")]
    public GameObject cuboIndicadorWin;
    public Material materialACambiarGood, materialBad, meterialVeryGood;
    public Material materialActual;
    
    [Header("Time")]
    public Text timeText;
    public float timeGame = 120;

    [Header("Particle")]
    public ParticleSystem particleObj;

    [Header("Prefab")]
    // Instanciar numeros puntos
    public GameObject instantiatePoint;
    public GameObject numberPrefab;

    // Fade
    public FadeImage fadeImage;

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
        //ime.timeScale = 0.3f;
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

    public void AddPoint(float pointsToAdd)
    {
        if(pointsToAdd > 0 && pointsToAdd < 30)
        {
            // Reproducioms particulas
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialACambiarGood;
            //Debug.Log("Funciona");
            RestetMultipli();
        }
        else if(pointsToAdd < 0)
        {
            // Fade
            fadeImage.FadeImageObj();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialBad;
            //Debug.Log("Funciona x2");
            RestetMultipli();
        }
        else if(pointsToAdd > 30)
        {
            // Reproducioms particulas
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = meterialVeryGood;
            //Debug.Log("Funciona");
        }
        // Se multiplica el Score
        pointsToAdd *= multipliNumber;

        // Mostramos en pantalla puntos
        ShowNumber(pointsToAdd);

        scoreGame += pointsToAdd;
        //Debug.Log("Score: " + scoreGame);

        // Score no < 0
        if(scoreGame <= 0)
        {
            scoreGame = 0;
        }
        
        scoreText.text = "Score: " + scoreGame;
        StartCoroutine(BackMaterial());
    }

    //Cambiamos materiasl
    IEnumerator BackMaterial()
    {
        yield return new WaitForSeconds(0.2f);

        cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialActual;
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
}

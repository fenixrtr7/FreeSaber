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
    public Material materialACambiarGood, materialBad, meterialVeryGood;
    public Material materialActual;
    
    [Header("Time")]
    public Text timeText;
    public float timeGame = 120;
    public ParticleSystem particleObj;

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
        // Reproducioms particulas
        
        // Mostramos en pantalla puntos
        ShowNumber(pointsToAdd);

        if(pointsToAdd > 0 && pointsToAdd < 30)
        {
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialACambiarGood;
            //Debug.Log("Funciona");
        }
        else if(pointsToAdd < 0)
        {
            // Fade
            fadeImage.FadeImageObj();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = materialBad;
            //Debug.Log("Funciona x2");
        }
        else if(pointsToAdd > 30)
        {
            particleObj.Play();

            //Cambiamos el material del cubo
            cuboIndicadorWin.GetComponent<MeshRenderer> ().material = meterialVeryGood;
            //Debug.Log("Funciona");
        }
        scoreGame += pointsToAdd;
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
    void ShowNumber(int pointsShow)
    {
        var clone = (GameObject)Instantiate(numberPrefab, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
        clone.GetComponent<DamageNumber>().damagePoints = pointsShow;
    }
}

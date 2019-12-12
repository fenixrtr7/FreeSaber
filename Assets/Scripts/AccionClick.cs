﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionClick : MonoBehaviour
{
    Vector3 iniPosition;
    Vector3 endPosition;

    [HideInInspector]
    public bool cubeVertical = false;
    [HideInInspector]
    public bool cubeHorizontal = false;
    [HideInInspector]
    public bool canDesroy = false;
    float timePass = 0; 
    // Tiempo limite para ya no contar
    public float timeLimit = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Screen width: " + Screen.width); // 174
        //Debug.Log("Screen height: " + Screen.height); // 309
    }

    // Update is called once per frame
    void Update()
    {
        // Temporizador para devolver a flaso valor
        if (canDesroy)
        {
            timePass += Time.deltaTime;
            if (timePass >= timeLimit)
            {
                canDesroy = false;
                timePass = 0;
            }
        }
    }

    public void ClickBoton() {
        iniPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
        //Debug.Log("Inicial " + iniPosition);
        cubeHorizontal = false;
        cubeVertical = false;

        canDesroy = false;
    }

    public void EndClick() {
        endPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
        //Debug.Log("End " + endPosition);
        CreatePlane();
    }

    public void CreatePlane() {

        //     //canDesroy = true;

        // Obtener distancias
        float distanciaX = Vector3.Distance(new Vector3(iniPosition.x,0,0), new Vector3(endPosition.x,0,0));
        float distanciaY = Vector3.Distance(new Vector3(0,iniPosition.y,0), new Vector3(0,endPosition.y,0));

        //Debug.Log("X: " + distanciaX + " Y: " + distanciaY);

        // Si nos desplazamos mas sobre el eje x = corte horizontal
        if (distanciaX > distanciaY)
        {
            canDesroy = true;
            cubeHorizontal = true;
            //Debug.Log("Podemos destruirlo Horizontal");
        }
        else if (distanciaX < distanciaY)
        {
            canDesroy = true;
            cubeVertical = true;
            //Debug.Log("Podemos destruirlo Vertical");
        }

        // if (iniPosition.y > endPosition.y || iniPosition.y < endPosition.y)
        // {
            
        // }

    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    Image imageFade;
    public Color currentColor;

    float timeWait = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        imageFade = GetComponent<Image>();
        currentColor = imageFade.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeImageObj()
    {
        imageFade.color = Color.Lerp(currentColor, Color.white, timeWait);
        StartCoroutine(BackFade());
    }

    IEnumerator BackFade()
    {
        yield return new WaitForSeconds(timeWait);

        Color currentColorRed = imageFade.color;
        imageFade.color = Color.Lerp(currentColorRed, currentColor, timeWait);
        //Debug.Log("Funciona FADE");
    }
}
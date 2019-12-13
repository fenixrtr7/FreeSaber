using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    Image imageFade;

    //public Color currentColor;
    // Tiempo de espera
    public float timeWait = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        imageFade = GetComponent<Image>();
        //currentColor = imageFade.color;
        imageFade.CrossFadeAlpha(0,timeWait,true);
    }

    public void FadeImageObj()
    {
        //imageFade.color = Color.Lerp(currentColor, Color.white, timeWait);
        imageFade.CrossFadeAlpha(1,timeWait,true);
        StartCoroutine(BackFade());
    }

    IEnumerator BackFade()
    {
        yield return new WaitForSeconds(timeWait);

        //Color currentColorRed = imageFade.color;
        //imageFade.color = Color.Lerp(currentColorRed, currentColor, timeWait);
        imageFade.CrossFadeAlpha(0,timeWait,true);
        //Debug.Log("Funciona FADE");
    }
}

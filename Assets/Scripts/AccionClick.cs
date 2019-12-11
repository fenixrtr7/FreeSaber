using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionClick : MonoBehaviour
{
    Vector3 iniPosition;
    Vector3 endPosition;

    [HideInInspector]
    public bool canDesroy = false;
    float timePass = 0, timeLimit = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("Inicial " + iniPosition);
        canDesroy = true;
    }

    public void EndClick() {
        endPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
        Debug.Log("End " + endPosition);
        canDesroy = false;
    }
}

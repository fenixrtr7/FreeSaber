using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionClick : MonoBehaviour
{
    Vector3 iniPosition;
    Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickBoton() {
        iniPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
        Debug.Log("Inicial " + iniPosition);
    }

    public void EndClick() {
        endPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
        Debug.Log("End " + endPosition);
    }
}

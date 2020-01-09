using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorCubeFloor : MonoBehaviour
{
    public GameObject objOn, objOff;
    Material mymat; 

    private void Start() 
    {
        mymat = objOn.GetComponent<Renderer>().material;

        OffFloor();
    }

    public void OnFloor(Color color)
    {
        objOff.GetComponent<MeshRenderer>().enabled = false;
        objOn.GetComponent<MeshRenderer>().enabled = true;

        // Dar color
        mymat.color = color;
        mymat.SetColor("_EmissionColor", color);
    }

    public void OffFloor()
    {
        objOff.GetComponent<MeshRenderer>().enabled = true;
        objOn.GetComponent<MeshRenderer>().enabled = false;
    }
}

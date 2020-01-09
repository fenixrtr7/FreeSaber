using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorCubeFloor : MonoBehaviour
{
    public GameObject objOn, objOff;

    private void Start() 
    {
        OffFloor();
    }

    public void OnFloor()
    {
        objOff.SetActive(false);
        objOn.SetActive(true);
        // Dar color
        
    }

    public void OffFloor()
    {
        objOff.SetActive(true);
        objOn.SetActive(false);
    }
}

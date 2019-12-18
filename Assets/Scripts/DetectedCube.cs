using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedCube : MonoBehaviour
{
    public Material materialChange;
    Transform childObj;

    AccionClick actionClick;
    SendPoints sendPoints;
    
    bool inZone = false;

    int penalization = -10;

    public bool isVertical = true;
    SpawnerLineCube spawnerLineCube;

    // Zones
    public bool zone1 = false, zone2 = false;

    private void Start() 
    {
        //this.GetComponentInChildren<MeshRenderer>().material;
        childObj = this.gameObject.transform.GetChild(0);

        actionClick = FindObjectOfType<AccionClick>();
        sendPoints = FindObjectOfType<SendPoints>();

        spawnerLineCube = FindObjectOfType<SpawnerLineCube>();
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Enter");
        if(other.CompareTag("Zone"))
        {
            //Debug.Log("Enter the zone");
            childObj.GetComponentInChildren<MeshRenderer>().material = materialChange;

            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Zone"))
        {
            inZone = false;
        }
    }

    private void Update() 
    {
        if ((isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) || 
        (!isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
        {
            // Cambia de posición al cubo
            spawnerLineCube.ChangeZone(gameObject);

             // Add Point
             sendPoints.SendPointsScore();
        }
        else if ((!isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) || 
        (isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
        {
            // Cambia de posición al cubo
            spawnerLineCube.ChangeZone(gameObject);

            // Penalizacón
            UI_Manager.sharedInstance.AddPoint(penalization);
        }
    }
}

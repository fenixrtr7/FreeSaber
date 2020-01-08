using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSlide : MonoBehaviour
{
    // Zones
    bool activeUpdate = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeUpdate)
        {
            if ((isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
            (!isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
            {
                activeUpdate = false;

                // Cambia de posición al cubo y material
                spawnerLineCube.ChangeZone(gameObject);

                // Add Point
                sendPoints.SendPointsScore();
            }
            else if ((!isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
            (isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
            {
                activeUpdate = false;

                // Cambia de posición al cubo
                spawnerLineCube.ChangeZone(gameObject);

                // Penalizacón
                UI_Manager.sharedInstance.AddPoint(penalization);
            }
            StartCoroutine(WaitTime());
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("Zone"))
        {
            //Debug.Log("Enter the zone");
            childObj.GetComponentInChildren<MeshRenderer>().material = materialChange;

            inZone = true;
        }
    }
}

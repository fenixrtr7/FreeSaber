﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedCube : MonoBehaviour
{
    public Material materialChange;
    public Material materialActual;
    Transform childObj;

    AccionClick actionClick;
    SendPoints sendPoints;

    bool inZone = false, inCondition = false;

    int penalization = -10;

    public bool isVertical = true;
    SpawnerLineCube spawnerLineCube;

    // Zones
    bool actuveUpdate = true;

    private void Start()
    {
        //this.GetComponentInChildren<MeshRenderer>().material;
        childObj = this.gameObject.transform.GetChild(0);

        actionClick = FindObjectOfType<AccionClick>();
        sendPoints = FindObjectOfType<SendPoints>();

        spawnerLineCube = FindObjectOfType<SpawnerLineCube>();
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            inZone = false;
        }
    }

    private void Update()
    {
        if (actuveUpdate)
        {
            if ((isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
            (!isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
            {
                actuveUpdate = false;

                // Cambia de posición al cubo
                spawnerLineCube.ChangeZone(gameObject);
                ChangeMaterialOriginal();

                // Add Point
                sendPoints.SendPointsScore();
                StartCoroutine(WaitTime());
            }
            else if ((!isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
            (isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
            {
                actuveUpdate = false;

                // Cambia de posición al cubo
                spawnerLineCube.ChangeZone(gameObject);
                ChangeMaterialOriginal();

                // Penalizacón
                UI_Manager.sharedInstance.AddPoint(penalization);
                StartCoroutine(WaitTime());
            }
        }
    }

    public void ChangeMaterialOriginal()
    {
        childObj.GetComponentInChildren<MeshRenderer>().material = materialActual;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.1f);
        actuveUpdate = true;
    }
}

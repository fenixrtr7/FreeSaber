using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSlide : MonoBehaviour
{
    int FAULT = -10;
    AccionClick actionClick;
    SpawnerLineCube spawnerLineCube;
    SendPoints sendPoints;

    // Zones
    bool inZone = false;

    // Start is called before the first frame update
    void Start()
    {
        actionClick = FindObjectOfType<AccionClick>();
        spawnerLineCube = FindObjectOfType<SpawnerLineCube>();
        sendPoints = FindObjectOfType<SendPoints>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("Cubo"))
        {
            inZone = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cubo"))
        {
            CutCube(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cubo"))
        {
            inZone = false;
        }
    }

    public void CutCube(GameObject cubo)
    {
        var cuboDestroy = cubo.GetComponent<Cube>();

        if ((cuboDestroy.isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
            (!cuboDestroy.isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
        {

            // Cambia de posición al cubo y material
            spawnerLineCube.ChangeZone(cubo);

            // Add Point
            sendPoints.SendPointsScore();
        }
        else if ((!cuboDestroy.isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) ||
        (cuboDestroy.isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
        {

            // Cambia de posición al cubo
            spawnerLineCube.ChangeZone(cubo);

            // Penalizacón
            UI_Manager.sharedInstance.AddPoint(FAULT);
        }
    }
}

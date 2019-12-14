using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedCube : MonoBehaviour
{
    public Material materialChange;
    Transform childObj;

    AccionClick actionClick;
    
    bool inZone = false;

    // Points
    int points = 20;
    int penalization = -10;

    public bool isVertical = true;
    SpawnerLineCube spawnerLineCube;

    private void Start() {
        //this.GetComponentInChildren<MeshRenderer>().material;
        childObj = this.gameObject.transform.GetChild(0);

        actionClick = FindObjectOfType<AccionClick>();

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
            //Destroy(gameObject);
            spawnerLineCube.ChangeZone(gameObject);

             // Add Point
             UI_Manager.sharedInstance.AddPoint(points);
        }
        else if ((!isVertical && inZone && actionClick.cubeVertical && actionClick.canDesroy) || 
        (isVertical && inZone && actionClick.cubeHorizontal && actionClick.canDesroy))
        {
            //Destroy(gameObject);
            spawnerLineCube.ChangeZone(gameObject);

            UI_Manager.sharedInstance.AddPoint(penalization);
        }
    }

    // void OnCollisionEnter(Collision other)
    //  {
    //      if(other.gameObject.name == "Line")
    //      {
    //         //Camera.main.GetComponent<AudioSource>().Play();
    //         Destroy(gameObject);
  
    //          //Instantiate(splashReference, randomPos, transform.rotation);
  
    //         /* Update Score */
  
    //         //scoreReference.text = (int.Parse(scoreReference.text) + 1).ToString();
    //      }
    //  }
}

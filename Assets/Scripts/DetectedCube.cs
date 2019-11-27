using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedCube : MonoBehaviour
{
    public Material materialChange;
    Transform childObj;

    private void Start() {
        //this.GetComponentInChildren<MeshRenderer>().material;
        childObj = this.gameObject.transform.GetChild(0);
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("Enter");
        if(other.CompareTag("Zone"))
        {
            //Debug.Log("Enter the zone");
            childObj.GetComponentInChildren<MeshRenderer>().material = materialChange;
        }
    }

    void OnCollisionEnter(Collision other)
     {
         if(other.gameObject.name == "Line")
         {
            //Camera.main.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
  
             //Instantiate(splashReference, randomPos, transform.rotation);
  
            /* Update Score */
  
            //scoreReference.text = (int.Parse(scoreReference.text) + 1).ToString();
         }
     }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotateCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Cubo"))
        {
            Debug.Log("Rotamos");
            StartCoroutine(other.GetComponent<Cube>().RotateCube());
        }
    }
}

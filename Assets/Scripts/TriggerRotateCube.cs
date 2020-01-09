using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotateCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        bool isChange  = (Random.value > 0.5f);

        if (other.CompareTag("Cubo") && isChange)
        {
            Debug.Log("Rotamos");
            other.GetComponentInChildren<Cube>().ActiveAnimationRotate(true);
        }
    }
}

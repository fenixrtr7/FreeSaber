using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationCollider : MonoBehaviour
{
    public bool inBox = false;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Cubo"))
        {
            inBox = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Cubo"))
        {
            inBox = false;
        }
    }
}

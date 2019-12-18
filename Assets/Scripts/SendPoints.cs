using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendPoints : MonoBehaviour
{
    // Points
    int points = 20, originalPoints;
    [HideInInspector]
    public bool multiPoints;
    public NotificationCollider iniCollider, EndCollider;

    private void Start() {
        originalPoints = points;
    }

    public void SendPointsScore()
    {
        if(iniCollider.inBox && EndCollider.inBox)
        {
            points *= 2;
        }

        UI_Manager.sharedInstance.AddPoint(points);

        points = originalPoints;
    }
}
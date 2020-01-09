using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroAfterTime : MonoBehaviour
{
    public float timeToDestroy;
    float timeCurrent;

    private void Start() {
        timeCurrent = timeToDestroy;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeCurrent -= Time.deltaTime;
        if(timeCurrent <= 0){
            gameObject.SetActive(false);
        }
    }
    private void OnEnable() {
        timeCurrent = timeToDestroy;
        Debug.Log("Se activa");
    }
}

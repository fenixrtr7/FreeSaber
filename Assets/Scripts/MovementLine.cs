using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLine : MonoBehaviour
{
    public float minLimitSpeed = 6.5f, maxLimitSpeed = 8;
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minLimitSpeed, maxLimitSpeed);
    }

    //Update is called oncse per frame
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //transform.position -= Time.deltaTime * transform.forward * speed;
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLine : MonoBehaviour
{
    public float minLimitSpeed = 6.5f, maxLimitSpeed = 8;
    float speedCube;
    
    // Start is called before the first frame update
    void Start()
    {
        speedCube = Random.Range(minLimitSpeed, maxLimitSpeed);
    }

    //Update is called oncse per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            transform.position -= Time.deltaTime * transform.forward * speedCube;
        }
    }
}

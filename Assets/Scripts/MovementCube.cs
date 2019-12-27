using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCube : MonoBehaviour
{
    public float speedCube = 7.5f, minLimitSpeed = 6.5f, maxLimitSpeed = 8;
    // Start is called before the first frame update
    void Start()
    {
        speedCube = Random.Range(minLimitSpeed, maxLimitSpeed);
    }

    // Update is called oncse per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            transform.position -= Time.deltaTime * transform.forward * speedCube;
        }
    }
}

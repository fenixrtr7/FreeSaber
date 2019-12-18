using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCube : MonoBehaviour
{
    public float speedCube = 7.5f;
    // Start is called before the first frame update
    void Start()
    {
        speedCube = Random.Range(7.5f, 11);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            transform.position -= Time.deltaTime * transform.forward * speedCube;
        }
    }
}

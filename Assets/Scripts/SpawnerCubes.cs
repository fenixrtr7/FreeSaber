using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCubes : MonoBehaviour
{
    public GameObject[] cubeObj;
    public Transform[] points;
    public float beat;
    private float timer;
    int numCube;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (timer > beat)
            {
                int randomNumPoint = Random.Range(0, cubeObj.Length);
                //Debug.Log("Random Point: " + randomNumPoint);

                // if (randomNumPoint == 1)
                // {
                // 	numCube = 1;
                // }else
                // {
                // 	numCube = 0;
                // }

                GameObject cube = Instantiate(cubeObj[randomNumPoint], points[0]);
                cube.transform.localPosition = Vector3.zero;
                //cube.transform.Rotate(transform.forward,90*Random.Range(0,4));
                timer -= beat;
            }
            timer += Time.deltaTime;
        }
    }
}

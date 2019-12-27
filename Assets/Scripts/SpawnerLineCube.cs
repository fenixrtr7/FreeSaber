using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLineCube : MonoBehaviour
{
    public List<GameObject> cubeList = new List<GameObject>();
    // iList.Add(2);
    // iList.Add(3);
    // iList.Add(5);
    public GameObject[] cubeObj;
    public int lengthCube = 10;

    Vector3 transformActual;
    int addPosition;

    // Start is called before the first frame update
    void Start()
    {
        transformActual = this.transform.position;

        RandomInstantiateCube();
    }
    void RandomInstantiateCube()
    {
        for (int i = 0; i < lengthCube; i++)
        {
            int j = Random.Range(0, cubeObj.Length);
            addPosition = i * 5;

            transformActual = new Vector3 (transform.position.x, transform.position.y, transform.position.z + addPosition); 

            GameObject cube = Instantiate(cubeObj[j], transformActual, Quaternion.identity, transform);
            cubeList.Add(cube);
        }
    }

    public void ChangeZone(GameObject objectChange)
    {
        addPosition += 5;
        objectChange.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + addPosition); 
    
        objectChange.GetComponent<DetectedCube>().ChangeMaterialOriginal();
    }
}

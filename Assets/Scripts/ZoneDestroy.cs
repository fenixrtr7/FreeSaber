using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDestroy : MonoBehaviour
{
    public FadeImage fadeImage;
    public int penalization = -10;

    SpawnerLineCube spawnerLineCube;
    // Start is called before the first frame update
    void Start()
    {
        spawnerLineCube = FindObjectOfType<SpawnerLineCube>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Cubo"))
        {
            UI_Manager.sharedInstance.AddPoint(penalization);
            fadeImage.FadeImageObj();

            spawnerLineCube.ChangeZone(other.gameObject);
        }
    }
}

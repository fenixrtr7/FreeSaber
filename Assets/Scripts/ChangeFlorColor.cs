using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFlorColor : MonoBehaviour
{
    Material[] mat = new Material[2];
    public GameObject[] obj;
    public Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            mat[i] = obj[i].GetComponent<Renderer>().material;
        }

        ChangeMaterial();
    }

    void ChangeMaterial()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            int j = Random.Range(0, colors.Length);

            // Dar color
            mat[i].color = colors[j];
            mat[i].SetColor("_EmissionColor", colors[j]);
        }
    }
}

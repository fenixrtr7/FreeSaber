using UnityEngine;
 using System.Collections;
  
 public class LinesHandler : MonoBehaviour
 {
     // https://github.com/tutsplus/UnityFruitCutter
     
     public Color c1 = Color.yellow;
     public Color c2 = Color.red;
    public Material materialLine;
  
     private GameObject lineGO;
     private LineRenderer lineRenderer;
    private int i = 0;
  
     void Start()
     {
         lineGO = new GameObject("Line");
        lineGO.AddComponent<LineRenderer>();
         lineRenderer = lineGO.GetComponent<LineRenderer>();
        lineRenderer.material = materialLine;//new Material(Shader.Find("Mobile/Particles/Additive"));
        lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.05F, 0);
        lineRenderer.SetVertexCount(0);
    }
     
    void Update()
     {
        if (Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);
  
            if(touch.phase == TouchPhase.Moved)
            {
                lineRenderer.SetVertexCount(i+1);
                Vector3 mPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.2f);
                lineRenderer.SetPosition(i, Camera.main.ScreenToWorldPoint(mPosition));
                i++;
                //Debug.Log("Numero vertice: " + i);
                 
                /* Add Collider */
                 
                BoxCollider bc = lineGO.AddComponent<BoxCollider>();
                bc.transform.position = lineRenderer.transform.position;
                bc.size = new Vector3(0.1f, 0.1f, 0.1f);
            }
             
            if(touch.phase == TouchPhase.Ended)
            {
                /* Remove Line */
                 
                lineRenderer.SetVertexCount(0);
                i = 0;
                 
                /* Remove Line Colliders */
                 
                BoxCollider[] lineColliders = lineGO.GetComponents<BoxCollider>();
                 
                foreach(BoxCollider b in lineColliders)
                {
                    Destroy(b);
                }
            }
        }
    }
 }

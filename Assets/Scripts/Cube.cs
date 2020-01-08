using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Header("Material")]
    public Material materialChange;
    public Material materialActual;
    Transform childObj;

    [Header("Position")]
    public bool isVertical = true;
    bool isVerticalDef;

    [Header("Rotation")]
    public float smoothTime;
    public float tiltAroundY = 100;
    // Animator
    Animator animator;

    // Zones
    bool activeUpdate = true;

    private void Start()
    {
        isVerticalDef = isVertical;
        //this.GetComponentInChildren<MeshRenderer>().material;
        childObj = this.gameObject.transform.GetChild(0);

        // Animator
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");
        if (other.CompareTag("Zone"))
        {
            //Debug.Log("Enter the zone");
            childObj.GetComponentInChildren<MeshRenderer>().material = materialChange;
        }
    }

    // Cambiar color del material
    public void ChangeMaterialOriginal()
    {
        childObj.GetComponentInChildren<MeshRenderer>().material = materialActual;
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.1f);
        activeUpdate = true;
    }

    public IEnumerator RotateCube()
    {
        //Anim rotate ###########

        //Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);

        // //Rotamos el cubo
        // transform.rotation = Quaternion.Slerp(transform.rotation, target, smoothTime);

        yield return null;
    }

    public void ActiveAnimationRotate(bool isRotate)
    {
        animator.SetBool("isRotate", isRotate);

        if (isRotate == true)
        {
            isVertical = !isVertical;  
        }
        else
        {
            isVertical = isVerticalDef;
        }
    }
}

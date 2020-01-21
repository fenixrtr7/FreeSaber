using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public float speed;
    public float damagePoints;

    public Text damageText;
    public Transform origPosition;

    private void Start()
    {
        //origPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //this.transform.position = new Vector3(this.transform.position.x, -this.transform.position.y * damageSpeed * Time.deltaTime, this.transform.position.z);
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }

    private void OnEnable()
    {
        transform.position = origPosition.transform.position;
        damageText.text = damagePoints.ToString();

        if (damagePoints < 0)
        {
            damageText.color = Color.red;
        }else
        {
            damageText.color = Color.white;
        }
    }
}

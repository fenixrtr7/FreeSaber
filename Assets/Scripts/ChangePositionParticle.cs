using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionParticle : MonoBehaviour
{
    public float timeWait = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(timeWait);

        transform.position = new Vector3(-0.1f, 2.07f, -9.71f);
    }
}

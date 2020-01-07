using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMusic : MonoBehaviour
{
    public AudioClip[] musicList;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        int x = Random.Range(0, musicList.Length);
        //Debug.Log("Music: " + x);

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicList[x];
        audioSource.Play();
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }
}

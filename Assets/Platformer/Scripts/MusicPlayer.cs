using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;
    public GameManager timer;
    private AudioSource audioSource;
    private bool speedUp = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();
    }

    private void Update()
    {
        if (timer.GetTimer() == 100)
        {
            speedUp = true;
            if (speedUp)
                SpeedUpMusic();
            speedUp = false;
            Thread.Sleep(1000);
        }
        
    }

    private void SpeedUpMusic()
    {
        audioSource.pitch *= 1.5f;
    }
}

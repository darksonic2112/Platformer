using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip endMusic;
    public GameManager timer;
    private AudioSource audioSource;
    private bool speedUp = false;
    
    void Start()
    {
        
    }

    public void RestartMusic()
    {
        //audioSource.Stop();
        //audioSource.Play();
    }

    public void EndMusic()
    {
        audioSource.Stop();
    }

    private void SpeedUpMusic()
    {
        //audioSource.pitch *= 1.5f;
    }
}

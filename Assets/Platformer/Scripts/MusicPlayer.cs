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

    public void RestartMusic()
    {
        audioSource.Stop(); // Stop the music if it's already playing
        audioSource.Play(); // Start playing the music from the beginning
    }

    private void SpeedUpMusic()
    {
        //audioSource.pitch *= 1.5f;
    }
}

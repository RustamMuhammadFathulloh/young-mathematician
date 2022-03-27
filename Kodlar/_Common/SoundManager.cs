﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayAudio(SoundEffectSO sound)
    {
        audioSource.clip = sound.clip;
        audioSource.Play();
    }


    

}

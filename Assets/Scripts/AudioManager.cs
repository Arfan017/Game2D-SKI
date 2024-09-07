using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public AudioClip Backsound;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // PlayAllAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.clip = Backsound;
        PlayAllAudio();
    }

    public void StopAllAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    public void PlayAllAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}

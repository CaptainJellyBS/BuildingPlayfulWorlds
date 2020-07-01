using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    public AudioSource lazorL;
    public AudioSource lazorR;
    public AudioSource music;

    public static AudioM Instance { get; private set; }
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;
    }

    public void PlayLaserSound()
    {
        lazorL.Play();
        lazorR.Play();
    }

    public void UpdateMasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

    public void UpdateSFXVolume(float newVolume)
    {
        lazorL.volume = newVolume;
        lazorR.volume = newVolume;
    }

    public void UpdateMusicVolume(float newVolume)
    {
        music.volume = newVolume;
    }
}

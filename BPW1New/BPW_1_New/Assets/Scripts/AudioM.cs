﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioM : MonoBehaviour
{
    public AudioSource lazorL;
    public AudioSource lazorR;
    public AudioSource music;

    public AudioSource[] voiceLines;

    public GameObject settingsPanel;

    public float MasterVolume { get; private set; }
    public float SFXVolume { get; private set; }
    public float MusicVolume { get; private set; }
    public float VoiceVolume { get; private set; }

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

        #region load audio settings from player prefs
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            UpdateMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        }
        else
        {
            UpdateMasterVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            UpdateSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
        else
        {
            UpdateSFXVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            UpdateMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        else
        {
            UpdateMusicVolume(0.5f);
        }

        if (PlayerPrefs.HasKey("VoiceVolume"))
        {
            UpdateVoiceVolume(PlayerPrefs.GetFloat("VoiceVolume"));
        }
        else
        {
            UpdateVoiceVolume(0.5f);
        }
        #endregion
    }

    public void PlayLaserSound()
    {
        lazorL.Play();
        lazorR.Play();
    }

    public void PlayVoiceLine(int i)
    {
        if (voiceLines[i].isPlaying) { return; }
        voiceLines[i].Play();
    }

    public bool IsLinePlaying(int i)
    {
        return voiceLines[i].isPlaying;
    }

    public void UpdateMasterVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
        PlayerPrefs.SetFloat("MasterVolume", newVolume);
        MasterVolume = newVolume;
    }

    public void UpdateSFXVolume(float newVolume)
    {
        lazorL.volume = newVolume;
        lazorR.volume = newVolume;
        PlayerPrefs.SetFloat("SFXVolume", newVolume);
        SFXVolume = newVolume;
    }
    public void UpdateVoiceVolume(float newVolume)
    {
        foreach(AudioSource a in voiceLines)
        {
            a.volume = newVolume;
        }
        PlayerPrefs.SetFloat("VoiceVolume", newVolume);
        VoiceVolume = newVolume;
    }

    public void UpdateMusicVolume(float newVolume)
    {
        music.volume = newVolume;
        PlayerPrefs.SetFloat("MusicVolume", newVolume);
        MusicVolume = newVolume;
    }
}

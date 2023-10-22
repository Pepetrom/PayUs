using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sounds[] music, sfx;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("CaveAmbience");
    }
    public void PlayMusic(string name)
    {
        Sounds soundIwant = Array.Find(music,x=>x.name == name);
        if(soundIwant != null)
        {
            musicSource.clip = soundIwant.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string name)
    {
        Sounds soundIwant = Array.Find(sfx, x => x.name == name);
        if (soundIwant != null)
        {
            sfxSource.PlayOneShot(soundIwant.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

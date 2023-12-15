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
    private bool isplayingBackgorund = false;

    private void Awake()
    {
        if (Instance == null)
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
        PlayBackgroundSFX();
    }

    public void PlayMusic(string name)
    {
        Sounds soundIwant = Array.Find(music, x => x.name == name);
        if (soundIwant != null)
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
    public void PlaySfxSpeedUp(string name, float pitch = 1.0f)
    {
        Sounds soundIwant = Array.Find(sfx, x => x.name == name);
        if (soundIwant != null)
        {
            sfxSource.pitch = pitch;
            sfxSource.PlayOneShot(soundIwant.clip);
            // Reset pitch to 1.0 after playing the sound
            //sfxSource.pitch = 1.0f;
        }
    }


    public float GetSfxClipLength(string name)
    {
        Sounds soundIwant = Array.Find(sfx, x => x.name == name);
        return soundIwant != null ? soundIwant.clip.length : 0f;
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

    private void PlayBackgroundSFX()
    {
        PlaySfx("CaveBackground");
        isplayingBackgorund = true;
        StartCoroutine(WaitForSoundEnd());
    }
    private IEnumerator WaitForSoundEnd()
    {
        yield return new WaitForSeconds(AudioManager.Instance.GetSfxClipLength("SingleFootstep"));
        isplayingBackgorund = false;
    }
}

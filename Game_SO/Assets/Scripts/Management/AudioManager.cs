using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic("MainMenu");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
            Debug.Log("No music found!");
        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
            Debug.Log("No soundfx found!");
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private Sound[] _musicSounds, _sfxSounds;
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
                
    }

    private void Start()
    {
        MusicVolume(PlayerPrefs.GetFloat("VolumeParameter", 1));
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(_musicSounds, x => x.name == name);

        if (sound != null)
        {
            _musicSource.clip = sound.clip;
            _musicSource.Play();
        }
    }
    public void PlaySound(string name)
    {
        Sound sound = Array.Find(_sfxSounds, x => x.name == name);

        if (sound != null)
        {
            _sfxSource.clip = sound.clip;
            _sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        _musicSource.volume = volume;
        _sfxSource.volume = volume;
    }
}

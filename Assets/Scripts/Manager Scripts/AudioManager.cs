using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public bool friendStart;
    public bool friendEnd;

    public bool principalStart;
    public bool principalGoodEnd;
    public bool principalBadEnd;

    public bool auntStart;
    public bool auntGoodEnd;
    public bool auntBadEnd;

    public bool doctorStart;
    public bool doctorGoodEnd;
    public bool doctorBadEnd;

    public bool therapistEnd;

    public bool battleEnd;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s != null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}

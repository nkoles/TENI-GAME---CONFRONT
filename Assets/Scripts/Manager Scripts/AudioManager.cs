using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public float musicVolume, sfxVolume, musicMaxVolume, sfxMaxVolume, musicMinVolume, sfxMinVolume, fadeOutDelay, fadeInDelay, pitchChange;
    public bool fadeOut, fadeIn, pitchUp, pitchDown;

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

    public bool mainTheme;
    public bool therapistEnd;
    public bool battleEnd;

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

    void FixedUpdate()
    {
        if(fadeIn)
        {
            musicSource.volume += (Time.fixedDeltaTime/fadeInDelay);
            //Debug.Log(musicSource.volume);

            if(musicSource.volume >= musicMaxVolume)
            {
                musicSource.volume = musicMaxVolume;
                fadeIn = false;
            }
        }
        else if(fadeOut)
        {
            musicSource.volume -= (Time.fixedDeltaTime/fadeOutDelay);
            //Debug.Log(musicSource.volume);

            if(musicSource.volume <= musicMinVolume)
            {
                musicSource.volume = musicMinVolume;
                fadeOut = false;
            }
        }

        if(pitchUp)
        {
            if(musicSource.pitch >= 1.05f)
            {
                pitchUp = false;
                musicSource.pitch = 1.05f;
            }
            else
            {
                musicSource.pitch += Time.fixedDeltaTime/pitchChange;
            }
        }
        else if(pitchDown)
        {
            if(musicSource.pitch >= .95f)
            {
                musicSource.pitch = .95f;
            }
            else
            {
                musicSource.pitch -= Time.fixedDeltaTime/pitchChange;
            }
        }
        else
        {
            if(musicSource.pitch >= 1.01f)
            {
                musicSource.pitch -= Time.fixedDeltaTime/pitchChange;
            }
            else if(musicSource.pitch <= 0.99f)
            {
                musicSource.pitch += Time.fixedDeltaTime/pitchChange;
            }
            else
            {
                musicSource.pitch = 1;
            }
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s != null)
        {
            StartCoroutine(FadeIn(s.clip));
            //musicSource.clip = s.clip;
            //musicSource.Play();
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

    public IEnumerator FadeIn(AudioClip clip)
    {
        musicVolume = musicSource.volume;

        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(fadeOutDelay*(musicVolume - musicMinVolume));

        musicSource.clip = clip;
        musicSource.Play();

        fadeIn = true;
        fadeOut = false;

        yield return new WaitForSeconds(fadeInDelay*(musicMaxVolume - musicVolume));
    }

    public IEnumerator FadeOut()
    {
        if(musicSource.clip != null && musicSource.volume > musicMinVolume)
        {
            musicVolume = musicSource.volume;

            fadeOut = true;

            yield return new WaitForSeconds(fadeOutDelay*(musicVolume - musicMinVolume));
        }
    }
}

using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEditor.Timeline.Actions;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in musicSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Sound s in sfxSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Music " + name + " not found!");
        }
        else
        {
            s.source.Play();
            Debug.Log(name + " Music playing");
        }
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Music " + name + " not found!");
        }
        else
        {
            s.source.Stop();
            Debug.Log(name + " Music stopped playing");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("SFX " + name + " not found!");
        }
        else
        {
            s.source.PlayOneShot(s.clip, s.volume);
            Debug.Log(name + " SFX playing");
        }
    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("SFX " + name + " not found!");
        }
        else
        {
            s.source.Stop();
            Debug.Log(name + " SFX stopped playing");
        }
    }

    
}



using UnityEngine.Audio;
using UnityEngine;

using System;
using Chronos;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    public static AudioManager instance { get; private set;  }

    
    void Awake()
    {
        // Add audio source components
        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if (!isPlaying(s.name)) s.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null || s.source == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if (isPlaying(s.name)) s.Stop();
    }

    public bool isPlaying(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return false;
        }
        return s.IsPlaying();
    }
}
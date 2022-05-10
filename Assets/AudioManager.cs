using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    void Awake()
    {
        foreach (var s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, x => x.Name == name);
        s.Source.Play();
    }
}

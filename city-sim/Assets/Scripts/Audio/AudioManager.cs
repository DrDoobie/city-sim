using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string name)
    {
        //Debug.Log("Playing sound " + name);

        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Play();
    }
}

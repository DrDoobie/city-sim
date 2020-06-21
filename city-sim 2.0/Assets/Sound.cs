using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 3.0f)]
    public float pitch;
    public string name;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
}

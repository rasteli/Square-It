using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string clipName;

    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

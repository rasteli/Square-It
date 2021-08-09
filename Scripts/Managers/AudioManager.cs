using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);

        if (sound == null)
        {
            Debug.LogWarning("Audio " + name + " could not be found!");
            return;
        }

        sound.source.pitch = sound.pitch;
        sound.source.volume = sound.volume;

        sound.source.Play();
    }

    public void Stop(string name)
    {
        Sound sound = GetSound(name);

        if (sound != null && sound.source.isPlaying)
        {
            sound.source.Stop();
        }
    }

    public void PauseUnPause(string name, string action)
    {
        Sound sound = GetSound(name);

        switch (action)
        {
            case "pause":
                if (sound.source.isPlaying)
                    sound.source.Pause();
                break;
            
            case "unpause":
                sound.source.UnPause();
                break;
        }
    }

    private Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.clipName == name);
    }
}

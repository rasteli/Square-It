using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Linq;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;
    public TMP_Dropdown resolutionOptions;
    public TMP_Dropdown gfxOptions;

    public Slider musicVolume;
    public Slider gameplayVolume;
    public Toggle fullscreen;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution{
            width = resolution.width,
            height = resolution.height
        }).Distinct().ToArray();

        resolutionOptions.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        Resolution currentResolution = Screen.currentResolution;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string res = resolutions[i].width + "x" + resolutions[i].height;

            options.Add(res);

            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionOptions.AddOptions(options);
        resolutionOptions.value = (int)SavePrefs.LoadState("ResolutionIndex", (float)currentResolutionIndex);
        resolutionOptions.RefreshShownValue();

        Screen.fullScreen = SavePrefs.LoadState("Fullscreen", 1) == 1;
        mixer.SetFloat("music", SavePrefs.LoadState("MusicVolume", 0));
        mixer.SetFloat("sfx", SavePrefs.LoadState("SoundVolume", 0));
        QualitySettings.SetQualityLevel((int)SavePrefs.LoadState("QualityIndex", 2));

        fullscreen.isOn = SavePrefs.LoadState("Fullscreen", 1) == 1;
        gfxOptions.value = (int)SavePrefs.LoadState("QualityIndex", 2);
        musicVolume.value = SavePrefs.LoadState("MusicVolume", 0);
        gameplayVolume.value = SavePrefs.LoadState("SoundVolume", 0);

        Screen.SetResolution(
            (int)SavePrefs.LoadState("Width", currentResolution.width),
            (int)SavePrefs.LoadState("Height", currentResolution.height),
            Screen.fullScreen
        );
    }

    public void SetMusic(float volume)
    {
        mixer.SetFloat("music", volume);
        SavePrefs.SaveState("MusicVolume", volume);
    }

    public void SetSound(float volume)
    {
        mixer.SetFloat("sfx", volume);
        SavePrefs.SaveState("SoundVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SavePrefs.SaveState("QualityIndex", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SavePrefs.SaveState("Fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        SavePrefs.SaveState("Width", resolution.width);
        SavePrefs.SaveState("Height", resolution.height);
        SavePrefs.SaveState("ResolutionIndex", resolutionIndex);
    }
}

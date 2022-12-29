using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown ResDropdown;

    Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();

        List<string> choices = new List<string>();
        int currentReso = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string choice = resolutions[i].width + "x" + resolutions[i].height;
            choices.Add(choice);

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentReso = i;
            }
        }
        ResDropdown.AddOptions(choices);
        ResDropdown.value = currentReso;
        ResDropdown.RefreshShownValue();
    }

    public void Resolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Volume (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void Fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
}

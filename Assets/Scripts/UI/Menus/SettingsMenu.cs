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

    public Slider volSlider;
    public Slider musSlider;
    public Slider sfxSlider;


    void Awake()
    {
        volSlider.value = PlayerPrefs.GetFloat("masterslidersavednumber");
        musSlider.value = PlayerPrefs.GetFloat("musslidersavednumber");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxslidersavednumber");

        
    }
    void Start()
    {
        resolutions = Screen.resolutions;
        ResDropdown.ClearOptions();

        List<string> choices = new List<string>();
        int currentReso = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string choice = resolutions[i].width + "x" + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            choices.Add(choice);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentReso = i;
            }
        }
        ResDropdown.AddOptions(choices);
        ResDropdown.value = currentReso;
        ResDropdown.RefreshShownValue();

        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("masterslidersavednumber"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musslidersavednumber"));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxslidersavednumber"));

    }

    void Update()
    {
        PlayerPrefs.SetFloat("masterslidersavednumber", (float)volSlider.value);
        PlayerPrefs.SetFloat("musslidersavednumber", (float)musSlider.value);
        PlayerPrefs.SetFloat("sfxslidersavednumber", (float)sfxSlider.value);


    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);

    }

    public void MusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
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

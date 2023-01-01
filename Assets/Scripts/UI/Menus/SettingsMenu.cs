using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown ResDropdown;
    public TMPro.TMP_Dropdown QualityDropdown;
    public Toggle fullscreentoggle;

    Resolution[] resolutions;
<<<<<<< Updated upstream
=======

    public Slider volSlider;
    public Slider musSlider;
    public Slider sfxSlider;
    

    void Awake()
    {
        volSlider.value = PlayerPrefs.GetFloat("masterslidersavednumber");
        musSlider.value = PlayerPrefs.GetFloat("musslidersavednumber");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxslidersavednumber");

        
    }
>>>>>>> Stashed changes
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

            if(resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentReso = i;
            }
        }
        ResDropdown.AddOptions(choices);
        ResDropdown.value = currentReso;
        ResDropdown.RefreshShownValue();
<<<<<<< Updated upstream
=======

        audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("masterslidersavednumber"));
        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musslidersavednumber"));
        audioMixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("sfxslidersavednumber"));

        int resolutionsaved = PlayerPrefs.GetInt("resIndex");
        ResDropdown.value = resolutionsaved;

        int qualitysaved = PlayerPrefs.GetInt("qualityIndex");
        QualityDropdown.value = qualitysaved;

        if (PlayerPrefs.GetInt("fullscreen") == 1)
        {
            fullscreentoggle.isOn = true;
        }
        else
        {
            fullscreentoggle.isOn = false;
        }
    }

    void Update()
    {
        PlayerPrefs.SetFloat("masterslidersavednumber", (float)volSlider.value);
        PlayerPrefs.SetFloat("musslidersavednumber", (float)musSlider.value);
        PlayerPrefs.SetFloat("sfxslidersavednumber", (float)sfxSlider.value);

        PlayerPrefs.SetInt("resIndex", ResDropdown.value);
        PlayerPrefs.SetInt("qualityIndex", QualityDropdown.value);
        if (Screen.fullScreen == true)
        {
            PlayerPrefs.SetInt("fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullscreen", 0);
        }
        

>>>>>>> Stashed changes
    }

    public void SetResolution(int resolutionIndex)
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
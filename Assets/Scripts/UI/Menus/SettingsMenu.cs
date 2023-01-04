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
    public Toggle cheats;

    public GameObject achievepopup;




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

        if (PlayerPrefs.GetInt("cheats") == 1)
        {
            cheats.isOn = true;
            cheats.enabled = false;
            cheats.gameObject.SetActive(false);

        }
        else
        {
            cheats.isOn = false;
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

        if (cheats.isOn == true)
        {
            PlayerPrefs.SetInt("cheats", 1);
            cheats.enabled = false;
            cheats.gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("cheatspopupdone") == 0)
            {
                achievepopup.SetActive(true);
                deleteAfterSeconds(2);
                PlayerPrefs.SetInt("cheatspopupdone", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("cheats", 0);
        }

        if (cheats.isOn == true && PlayerPrefs.GetInt("cheatspopupdone") == 1)
        {
            PlayerPrefs.SetInt("cheatspopupdone", 1);
        }
        else
        {
            PlayerPrefs.SetInt("cheatspopupdone", 0);
        }

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

    void DeletePopUp()
    {
        achievepopup.SetActive(false);
    }
    void deleteAfterSeconds(float seconds)
    {
        StartCoroutine(_PlayAfterSeconds(seconds));
    }
    IEnumerator _PlayAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DeletePopUp();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaved : MonoBehaviour
{
    public Slider volSlider;
    // Start is called before the first frame update
    void Awake()
    {
        volSlider.value = PlayerPrefs.GetFloat("masterslidersavednumber");
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("masterslidersavednumber", (float)volSlider.value);
    }
}

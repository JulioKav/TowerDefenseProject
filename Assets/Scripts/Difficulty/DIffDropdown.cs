using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIffDropdown : MonoBehaviour
{

    public TMPro.TMP_Dropdown difficultyDropdown;
    // Start is called before the first frame update
    private void Awake()
    {
        int diffsaved = PlayerPrefs.GetInt("difficultyIndex");
        difficultyDropdown.value = diffsaved;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("difficultyIndex", difficultyDropdown.value);
        if (difficultyDropdown.value != 2)
        {
            
            PlayerPrefs.SetInt("difficultyachieve", 1);
        }
        
    }

    public void difficulty(int diffsaved)
    {
        difficultyDropdown.value = diffsaved;
    }
}

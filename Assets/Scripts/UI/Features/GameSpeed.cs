using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public TMPro.TMP_Dropdown GameSpeedDropdown;
    public GameObject pauseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
            if (GameSpeedDropdown.value == 0)
            {
                Time.timeScale = 1f;
            }
            if (GameSpeedDropdown.value == 1)
            {
                Time.timeScale = 1.5f;
            }
            if (GameSpeedDropdown.value == 2)
            {
                Time.timeScale = 2f;
            }
        
        
        
      
    }

    public void setRate(int rate)
    {
        
        GameSpeedDropdown.value = rate;
    }
}

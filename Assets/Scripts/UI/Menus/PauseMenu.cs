using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;
    public GameObject HelpUI;
    public GameObject skillpoints;
    public GameObject Gamespeed;
    public GameObject achieveButtonui;
    public GameObject skillpointsTxT;
    public GameObject inventory;
    public GameObject nextwaveui;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePaused)
            {
                Resume();
                
            }
            else
            {
                Pause();
                
            }
        }

        
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GamePaused = false;
    }

    void Pause()
    {
        
        PauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Help()
    {

        HelpUI.SetActive(true);
        skillpoints.SetActive(false);
        Gamespeed.SetActive(false);
        achieveButtonui.SetActive(false);
        skillpointsTxT.SetActive(false);
        inventory.SetActive(false);
        nextwaveui.SetActive(false);
    }

    public void backtoPause()
    {
        HelpUI.SetActive(false);
        skillpoints.SetActive(true);
        Gamespeed.SetActive(true);
        achieveButtonui.SetActive(true);
        skillpointsTxT.SetActive(true);
        inventory.SetActive(true);
        nextwaveui.SetActive(true);
    }
}

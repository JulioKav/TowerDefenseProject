using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject PauseMenuUI;
    public TMPro.TMP_Dropdown GameSpeedDropdown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
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
        Time.timeScale = 1f;
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
        GamePaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
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

    private void Start()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Achievements : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject achieveMenuUI;
    public GameObject skillpoints;
    public Button achieveButton;
    public GameObject achievepopup;
    public int kills;
    public GameObject firstkillachieveObject;
    public GameObject tenkillachieveObject;

    protected static bool firstkillachieve = false;
    //public Button returnButton;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
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

        if (kills > 1)
        {
            firstkillachieve = true;
            
        }
        AchieveChecker();
        if (firstkillachieveObject != null)
        {
            PlayerPrefs.SetInt("firstkillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("firstkillachieve", 1);
        }

        
    }
    public void Resume()
    {
        achieveMenuUI.SetActive(false);
        skillpoints.SetActive(true);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        achieveMenuUI.SetActive(true);
        skillpoints.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        
    }

    public void AchieveChecker()
    {
        if(firstkillachieve == true && firstkillachieveObject != null && PlayerPrefs.GetInt("firstkillachieve") == 0)
        {
            Destroy(firstkillachieveObject);
            PlayerPrefs.SetInt("firstkillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);

        }
    }

    private void Start()
    {
        
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

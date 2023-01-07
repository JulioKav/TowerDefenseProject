using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultACHIEVE : MonoBehaviour
{
    public TMPro.TMP_Dropdown difficulty;

    public GameObject achievepopup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (difficulty.value != 2 || difficulty.value != 6)
        {
            PlayerPrefs.SetInt("difficultyachieve", 1);
            
            
            if (PlayerPrefs.GetInt("difficultypopupdone1") == 0)
            {
                achievepopup.SetActive(true);
                deleteAfterSeconds(2);
                PlayerPrefs.SetInt("difficultypopupdone1", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("difficultyachieve", 0);
        }

        if ((difficulty.value != 2 || difficulty.value != 6) && PlayerPrefs.GetInt("difficultypopupdone1") == 1)
        {
            PlayerPrefs.SetInt("difficultypopupdone1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("difficultypopupdone1", 0);
        }

        //harddoifficulty
        if (difficulty.value == 6)
        {
            PlayerPrefs.SetInt("harddifficultyachieve", 1);


            if (PlayerPrefs.GetInt("difficultypopupdone2") == 0)
            {
                achievepopup.SetActive(true);
                deleteAfterSeconds(2);
                PlayerPrefs.SetInt("difficultypopupdone2", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("harddifficultyachieve", 0);
        }

        if (difficulty.value == 6 && PlayerPrefs.GetInt("difficultypopupdone2") == 1)
        {
            PlayerPrefs.SetInt("difficultypopupdone2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("difficultypopupdone2", 0);
        }

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

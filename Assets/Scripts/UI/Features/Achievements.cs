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
    public GameObject Gamespeed;
    public GameObject achieveButtonui;
    public Button achieveButton;
    public GameObject achievepopup;

    private SkillManager skillManager;


    public GameObject firstkillachieveObject;
    public GameObject fiftykillachieveObject;
    public GameObject twofiftykillachieveObject;
    public GameObject all4typeskillachieveObject;
    public GameObject cheatergameobject;
    public GameObject onemageunlockgameobject;
    public GameObject finalskillgameobject;

    [HideInInspector]
    public int kills = 0;
    [HideInInspector]
    public int mechkills = 0;
    [HideInInspector]
    public int imaginarykills = 0;
    [HideInInspector]
    public int magickills = 0;
    [HideInInspector]
    public int physkills = 0;
    [HideInInspector]
    public bool onemageunlock = false;
    [HideInInspector]
    public bool finalskillunlock = false;

    protected static bool firstkillachieve = false;
    protected static bool fiftykillachieve = false;
    protected static bool twofiftykillachieve = false;
    protected static bool all4typeskillachieve = false;


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

        if (kills > 50)
        {
            fiftykillachieve = true;

        }


        if (kills > 250)
        {
            twofiftykillachieve = true;

        }

        if (mechkills >= 5 && physkills >= 5 && magickills >= 5 && imaginarykills >= 5)
        {
           
            all4typeskillachieve = true;

        }

       

        AchieveChecker();

    }
    public void Resume()
    {
        achieveMenuUI.SetActive(false);
        skillpoints.SetActive(true);
        Gamespeed.SetActive(true);
        achieveButtonui.SetActive(true);
        //Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        achieveMenuUI.SetActive(true);
        skillpoints.SetActive(false);
        Gamespeed.SetActive(false);
        achieveButtonui.SetActive(false);
        //Time.timeScale = 0f;
        GamePaused = true;

    }

    public void AchieveChecker()
    {
        //First Kill
        if (firstkillachieveObject != null)
        {
            PlayerPrefs.SetInt("firstkillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("firstkillachieve", 1);
            
        }

        if (firstkillachieve == true && firstkillachieveObject != null && PlayerPrefs.GetInt("firstkillachieve") == 0)
        {
            
            Destroy(firstkillachieveObject);
            PlayerPrefs.SetInt("firstkillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            skillManager.AddSkillPoints(25);

        }

        // 50 Kills
        if (fiftykillachieveObject != null)
        {
            PlayerPrefs.SetInt("fiftykillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("fiftykillachieve", 1);

        }

        if (fiftykillachieve == true && fiftykillachieveObject != null && PlayerPrefs.GetInt("fiftykillachieve") == 0)
        {

            Destroy(fiftykillachieveObject);
            PlayerPrefs.SetInt("fiftykillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            skillManager.AddSkillPoints(250);
        }

        // 250 Kills
        if (twofiftykillachieveObject != null)
        {
            PlayerPrefs.SetInt("twofiftykillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("twofiftykillachieve", 1);

        }

        if (twofiftykillachieve == true && twofiftykillachieveObject != null && PlayerPrefs.GetInt("twofiftykillachieve") == 0)
        {

            Destroy(twofiftykillachieveObject);
            PlayerPrefs.SetInt("twofiftykillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            skillManager.AddSkillPoints(1000);

        }

        //4 types kills
        if (all4typeskillachieveObject != null)
        {
            PlayerPrefs.SetInt("all4typeskillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("all4typeskillachieve", 1);

        }

        if (all4typeskillachieve == true && all4typeskillachieveObject != null && PlayerPrefs.GetInt("all4typeskillachieve") == 0)
        {

            Destroy(all4typeskillachieveObject);
            PlayerPrefs.SetInt("all4typeskillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            skillManager.AddSkillPoints(400);

        }
        //cheats
        if(PlayerPrefs.GetInt("cheatspopupdone") == 1)
        {
            Destroy(cheatergameobject);
        }
        //first mage

        if (onemageunlockgameobject != null)
        {
            PlayerPrefs.SetInt("firstmageachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("firstmageachieve", 1);

        }

        if (onemageunlock == true && onemageunlockgameobject != null && PlayerPrefs.GetInt("firstmageachieve") == 0)
        {

            Destroy(onemageunlockgameobject);
            PlayerPrefs.SetInt("firstmageachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            skillManager.AddSkillPoints(50);

        }

        //final skill

        if (finalskillgameobject != null)
        {
            PlayerPrefs.SetInt("finalskillachieve", 0);
        }
        else
        {
            PlayerPrefs.SetInt("finalskillachieve", 1);

        }

        if (finalskillunlock == true && finalskillgameobject != null && PlayerPrefs.GetInt("finalskillachieve") == 0)
        {

            Destroy(finalskillgameobject);
            PlayerPrefs.SetInt("finalskillachieve", 1);
            achievepopup.SetActive(true);
            deleteAfterSeconds(2);
            

        }

        

    }

    private void Awake()
    {
        /*
        if (PlayerPrefs.GetInt("firstkillachieve") == 1)
        {
            Destroy(firstkillachieveObject);
        }

        if (PlayerPrefs.GetInt("fiftykillachieve") == 1)
        {
            Destroy(firstkillachieveObject);
        }
        */
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

    private void Start()
    {
        skillManager = GameObject.FindObjectsOfType<SkillManager>()[0];
    }
}

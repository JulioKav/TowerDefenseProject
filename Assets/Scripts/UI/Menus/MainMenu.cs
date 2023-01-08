using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    public GameObject[] levels;
    GameObject selectedLevel = null;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void backToMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void level1()
    {
        selectedLevel = Instantiate(levels[0], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(selectedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void level2()
    {
        selectedLevel = Instantiate(levels[1], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(selectedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void level3()
    {
        selectedLevel = Instantiate(levels[2], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(selectedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void level4()
    {
        selectedLevel = Instantiate(levels[3], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(selectedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void level5()
    {
        selectedLevel = Instantiate(levels[4], Vector3.zero, Quaternion.identity);
        DontDestroyOnLoad(selectedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UnloadLevel()
    {
        Destroy(selectedLevel);
        selectedLevel = null;
    }


}

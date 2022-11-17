using UnityEngine;

public class DisableOnGameEnd : MonoBehaviour
{
    // This scripts subscribes the attached game object to the GameEndEvent, and disables it on game end
    void OnEnable()
    {
        SkillManager.OnGameEnd += GameEndHandler;
    }

    void OnDisable()
    {
        SkillManager.OnGameEnd -= GameEndHandler;
    }

    void GameEndHandler(int status)
    {
        gameObject.SetActive(false);
    }
}

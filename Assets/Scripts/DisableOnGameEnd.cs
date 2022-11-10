using UnityEngine;

public class DisableOnGameEnd : MonoBehaviour
{
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

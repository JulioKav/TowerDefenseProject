using UnityEngine;

public class DisableOnGameEnd : MonoBehaviour
{
    // This scripts subscribes the attached game object to the GameEndEvent, and disables it on game end
    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.WIN:
            case GameState.LOSE:
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}

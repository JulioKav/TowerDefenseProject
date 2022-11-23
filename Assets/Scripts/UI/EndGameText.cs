using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameText : MonoBehaviour
{

    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable() { GameStateManager.OnStateChange += StateChangeHandler; }
    void OnDisable() { GameStateManager.OnStateChange -= StateChangeHandler; }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.WIN:
                text.text = "YOU WIN!";
                break;
            case GameState.LOSE:
                text.text = "YOU LOSE!";
                break;
            case GameState.PRE_ROUND:
                StartCoroutine(PreRoundTimer());
                break;
            case GameState.IDLE:
                text.text = "";
                break;
            default:
                break;
        }
    }

    IEnumerator PreRoundTimer()
    {
        int timerLength = GameStateManager.Instance.PreRoundTimeInSeconds;
        while (timerLength > 0)
        {
            text.text = timerLength + "";
            yield return new WaitForSeconds(1);
            timerLength--;
        }
        text.text = "";
    }
}

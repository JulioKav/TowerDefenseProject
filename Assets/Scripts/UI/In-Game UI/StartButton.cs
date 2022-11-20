using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public WaveSpawner waveSpawner;

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
            case GameState.IDLE:
                GetComponent<Button>().interactable = true;
                break;
            default:
                break;
        }
    }

    public void TriggerNextWave()
    {
        GetComponent<Button>().interactable = false;
        GameStateManager.Instance.StartRound();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour
{
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

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
                UpdateWaveNumber();
                break;
            default:
                break;
        }
    }

    void UpdateWaveNumber()
    {
        text.text = "Next Wave: " + (WaveSpawner.WaveNumber + 1);
    }
}

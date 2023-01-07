using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGraphicDisabler : MonoBehaviour
{
    public Color enabledColor;
    public Color disabledColor;

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
            case GameState.PRE_ROUND:
                GetComponent<Image>().color = disabledColor;
                break;
            case GameState.IDLE:
                GetComponent<Image>().color = enabledColor;
                break;
            default:
                break;
        }
    }
}

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

    void OnEnable() { SkillManager.OnGameEnd += GameEndHandler; }
    void OnDisable() { SkillManager.OnGameEnd -= GameEndHandler; }

    void GameEndHandler(int status)
    {
        text.text = (status == 1) ? "YOU WIN!" : "YOU LOSE!";
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionManager : MonoBehaviour
{
    public static SkillDescriptionManager Instance;
    void Awake() { if (!Instance) Instance = this; }

    TextMeshProUGUI tmp;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ClearText();
    }

    public void SetText(string name, string description, int cost)
    {
        string text = "Name: " + name + "\nDescription: " + description + "\nCost: " + cost + " Skill Points";
        tmp.text = text;
    }

    public void ClearText()
    {
        tmp.text = "Hover a skill to see its info";
    }
}

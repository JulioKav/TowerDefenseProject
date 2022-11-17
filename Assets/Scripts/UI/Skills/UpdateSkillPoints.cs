using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateSkillPoints : MonoBehaviour
{

    TextMeshProUGUI skillPointText;
    public SkillManager skillManager;

    void Start()
    {
        skillPointText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        skillPointText.text = "Skill Points: " + skillManager.skillPoints;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateSkillPoints : MonoBehaviour
{

    TextMeshProUGUI skillPointText;

    void Start()
    {
        skillPointText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        skillPointText.text = "Skill Points: " + SkillManager.Instance.skillPoints;
    }
}

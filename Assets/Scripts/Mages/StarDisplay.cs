using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{

    public Image[] stars;

    void Start()
    {
        stars = transform.GetComponentsInChildren<Image>();
    }

    public void UnlockSkill(int skillId)
    {
        stars[skillId].color = Color.white;
    }

    public void LockSkill(int skillId)
    {
        stars[skillId].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
}

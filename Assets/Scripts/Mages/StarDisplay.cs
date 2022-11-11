using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    // Responsible for the star display above the mage tower

    public Image[] stars;

    void Start()
    {
        stars = transform.GetComponentsInChildren<Image>();
    }

    public void UnlockSkill(int skillId)
    {
        // Makes star fully visible
        stars[skillId].color = Color.white;
    }

    public void LockSkill(int skillId)
    {
        // Make star translucent
        stars[skillId].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
}

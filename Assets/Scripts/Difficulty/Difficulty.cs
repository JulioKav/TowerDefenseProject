using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public TMPro.TMP_Dropdown difficultyDropdown;
    public static float bullet_dmg = 30;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (difficultyDropdown.value == 0)
        {
            bullet_dmg = bullet_dmg * 1;
        }
        if (difficultyDropdown.value == 1)
        {
            bullet_dmg = bullet_dmg * 2;
        }
        if (difficultyDropdown.value == 2)
        {
            bullet_dmg = bullet_dmg * 3;
        }
    }

    // Update is called once per frame
    
}

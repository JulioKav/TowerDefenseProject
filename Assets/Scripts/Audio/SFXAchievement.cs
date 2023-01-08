using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAchievement : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        AudioManager.instance.PlaySoundEffect("Achievement");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffect : MonoBehaviour
{
    public enum SoundEffects
    {
        Click, Impact, Boon, Achievement
    }

    public SoundEffects sfx;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.instance.PlaySoundEffect(sfx.ToString()));
    }
}

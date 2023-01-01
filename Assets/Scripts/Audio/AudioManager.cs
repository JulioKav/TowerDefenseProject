using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
<<<<<<< Updated upstream
=======

    bool boonSelection = false;

    public float MusicVolume { get { return _musicVolume; } set { _musicVolume =   Math.Clamp(value, 0f, 0.5f); } }
    private float _musicVolume = 0.5f;

>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
<<<<<<< Updated upstream
        

=======

        DontDestroyOnLoad(gameObject);
>>>>>>> Stashed changes

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s==null)
        {
            Debug.LogWarning("sound" + name + "not found!");
            return;
        }
        s.source.Play();
    }
}

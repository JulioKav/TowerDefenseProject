using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    bool boonSelection = false;

    public float MusicVolume { get { return _musicVolume; } set { _musicVolume = Math.Clamp(value, 0f, 1f); } }
    private float _musicVolume = 1f;

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

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;



        }


    }

    void Start()
    {
        PlayAfterSeconds("Idle", 3);
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += GameStateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= GameStateChangeHandler;
    }

    void GameStateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.IDLE:
                PlayAfterSeconds("Idle", 3);
                break;
            case GameState.PRE_ROUND:
                Stop("Idle", GameStateManager.Instance.PreRoundTimeInSeconds + 1);
                break;
            case GameState.ROUND_ONGOING:
                Play("WaveOngoing");
                break;
            case GameState.POST_ROUND:
                Stop("WaveOngoing", GameStateManager.Instance.PostRoundTimeInSeconds);
                break;
            default:
                break;
        }
    }

    public void Stop(string name, float fadeSeconds)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound" + name + "not found!");
            return;
        }
        StartCoroutine(FadeSoundInSeconds(s, fadeSeconds));
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound" + name + "not found!");
            return;
        }
        s.source.Play();
    }

    IEnumerator FadeSoundInSeconds(Sound s, float seconds)
    {
        int decrementsPerSecond = 30;
        float timeStep = 1.0f / decrementsPerSecond;
        int steps = (int)(decrementsPerSecond * seconds) + 1;
        float decrement = s.source.volume / steps;
        for (int i = 0; i < steps; i++)
        {
            s.source.volume -= decrement;
            yield return new WaitForSeconds(timeStep);
        }
        s.source.Stop();
        s.source.volume = MusicVolume;
    }

    void PlayAfterSeconds(string name, float seconds)
    {
        StartCoroutine(_PlayAfterSeconds(name, seconds));
    }
    IEnumerator _PlayAfterSeconds(string name, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Play(name);
    }
}

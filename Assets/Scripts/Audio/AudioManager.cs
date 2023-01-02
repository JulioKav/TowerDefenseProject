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

    private bool _attack = false;
    public bool Attack
    {
        get { return _attack; }
        set
        {
            if (_attack && _attack == value) return;
            _attack = value;
            if (_attack) Play("WaveOngoing");
            else Stop("WaveOngoing", GameStateManager.Instance.PostRoundTimeInSeconds);
        }
    }

    private bool _idle = false;
    public bool Idle
    {
        get { return _idle; }
        set
        {
            if (_idle && _idle == value) return;
            _idle = value;
            if (_idle) PlayAfterSeconds("Idle", 2.5f);
            else Stop("Idle", GameStateManager.Instance.PreRoundTimeInSeconds + 1);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            return;
        }



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
                Idle = true;
                break;
            case GameState.PRE_ROUND:
                Idle = false;
                break;
            case GameState.PRE_GAME:
            case GameState.PRE_ROUND_DIALOGUE:
            case GameState.ROUND_ONGOING:
                Attack = true;
                break;
            case GameState.POST_ROUND:
                Attack = false;
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

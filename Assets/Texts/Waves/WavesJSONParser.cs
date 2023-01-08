using UnityEngine;

public class WavesJSONParser : MonoBehaviour
{
    public static WavesJSONParser Instance { get; private set; }
    void InitSingleton() { if (!Instance) Instance = this; }

    public TextAsset jsonFile;

    public Waves wavesJson { get; private set; }

    void Awake()
    {
        InitSingleton();
        wavesJson = JsonUtility.FromJson<Waves>(jsonFile.text);
    }

    [System.Serializable]
    public class Waves
    {
        public Wave[] waves;
    }

    [System.Serializable]
    public class Wave
    {
        public int bat, broom, cauldron, evilbook, eyeball, spider;
        public int frogboss;
    }
}
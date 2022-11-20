using UnityEngine;

public class BoonsJSONParser : MonoBehaviour
{
    public static BoonsJSONParser Instance { get; private set; }
    void InitSingleton() { if (!Instance) Instance = this; }

    public TextAsset jsonFile;

    public Boons boonsJson { get; private set; }

    void Awake()
    {
        InitSingleton();
        boonsJson = JsonUtility.FromJson<Boons>(jsonFile.text);
    }

    [System.Serializable]
    public class Boons
    {
        public int startingValue, valueIncrease;
        public Towers[] towers;
    }

    [System.Serializable]
    public class Towers
    {
        public string name;
        public string id;
        public int value;
    }
}
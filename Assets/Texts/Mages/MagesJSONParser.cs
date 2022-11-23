using UnityEngine;

public class MagesJSONParser : MonoBehaviour
{
    public static MagesJSONParser Instance { get; private set; }
    void InitSingleton() { if (!Instance) Instance = this; }

    public TextAsset jsonFile;

    public Mages magesJson { get; private set; }

    void Awake()
    {
        InitSingleton();
        magesJson = JsonUtility.FromJson<Mages>(jsonFile.text);
    }

    [System.Serializable]
    public class Mages
    {
        public Mage[] mages;
        public Skill finalSkill;
    }

    [System.Serializable]
    public class Mage
    {
        public string type;
        public string name;
        public int cost;
        public string description;
        public Skill[] skills;
    }

    [System.Serializable]
    public class Skill
    {
        public string id;
        public string name;
        public int cost;
        public string description;
    }
}
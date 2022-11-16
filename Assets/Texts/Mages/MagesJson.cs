using UnityEngine;

public class MagesJson : MonoBehaviour
{
    public static MagesJson Instance { get; private set; }

    public TextAsset jsonFile;

    public Mages magesJson { get; private set; }

    void Awake()
    {
        if (!Instance) Instance = this;
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
        public string name;
        public string type;
        public Skill[] skills;
    }

    [System.Serializable]
    public class Skill
    {
        public string name;
        public string cost;
    }
}
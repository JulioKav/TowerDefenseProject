using UnityEngine;

public class DialoguesJSONParser : MonoBehaviour
{
    public static DialoguesJSONParser Instance { get; private set; }
    void InitSingleton() { if (!Instance) Instance = this; }

    public TextAsset jsonFile;

    public Dialogues dialoguesJson { get; private set; }

    void Awake()
    {
        InitSingleton();
        dialoguesJson = JsonUtility.FromJson<Dialogues>(jsonFile.text);
    }

    [System.Serializable]
    public class Dialogues
    {
        public StartOfGameDialogue[] startOfGame;
        public string finalWaveWithoutWeapon;
        public string defeat;
        public string finalWaveWithWeapon;
        public string victory;
        public string[] startOfRound;

        [System.Serializable]
        public class StartOfGameDialogue
        {
            public string[] option_1;
            public string[] option_2;
            public string[] option_3;
        }
    }
}
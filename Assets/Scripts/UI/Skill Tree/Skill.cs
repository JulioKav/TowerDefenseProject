using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public static int MAGE_COST = 50;
    public static int[] COSTS = new int[] { 200, 350, 500 };
    public static int FINAL_SKILL_COST = 1000;

    [HideInInspector] public List<Skill> nextSkills;

    [HideInInspector] public Button button;
    [HideInInspector] public MageClass mageClass;
    [HideInInspector] public int cost;

    protected string skillName;
    protected string skillDesc; // ...ription

    public bool isFirstSkill = false;
    public bool completesBranch = false;

    private bool _unlockable;
    public bool Unlockable { get { return _unlockable; } set { _unlockable = value; UpdateButtonAppearance(); } }
    private bool _unlocked;
    public bool Unlocked { get { return _unlocked; } set { _unlocked = value; UpdateButtonAppearance(); } }

    protected MagesJSONParser.Mage mageJson;
    protected SkillManager SM;

    public void Start()
    {
        InitButton();
        InitStats();
        InitNextSkills();
        SM = SkillManager.Instance;
    }

    void InitButton()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TryUnlockSkill);
    }

    void InitStats()
    {
        mageJson = MagesJSONParser.Instance.magesJson.mages[(int)mageClass];
        if (isFirstSkill)
        {
            cost = MAGE_COST;
            skillName = mageJson.name;
            skillDesc = mageJson.description;
        }
        else foreach (var skillJson in mageJson.skills) if (gameObject.name.StartsWith(skillJson.id))
                {
                    string level = gameObject.name.Substring(skillJson.id.Length, gameObject.name.Length - skillJson.id.Length);
                    cost = COSTS[level.Length];
                    skillName = skillJson.name + " " + level;
                    skillDesc = skillJson.description;
                }
        Unlockable = isFirstSkill;
        Unlocked = false;
    }

    void InitNextSkills()
    {
        nextSkills = new List<Skill>();
        foreach (Transform skillT in transform)
        {
            Skill s;
            if (skillT.TryGetComponent<Skill>(out s)) nextSkills.Add(s);
        }
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }
    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.PRE_ROUND:
            case GameState.IDLE:
                UpdateButtonAppearance();
                break;
            default:
                break;
        }
    }

    public virtual void TryUnlockSkill()
    {
        if (SM.TryUnlockSkill(this))
        {
            Unlocked = true;
            if (completesBranch) FinalSkill.Instance.CheckUnlockable();
            foreach (Skill ns in nextSkills) ns.Unlockable = true;
            if (isFirstSkill) SM.mageSpawner.SpawnMage(mageClass);
        }
    }

    public void LockSkill()
    {
        foreach (Skill ns in nextSkills) ns.Unlockable = false;
        Unlocked = false;
        if (completesBranch) FinalSkill.Instance.CheckUnlockable();
    }

    void UpdateButtonAppearance()
    {
        // Udpate button based on if it is unlockable and/or unlocked
        button.interactable = GameStateManager.Instance.State == GameState.IDLE && Unlockable && !Unlocked;
        // Disable buttons based on interactability, but not game state
        GetComponent<Image>().enabled = Unlockable && !Unlocked;
    }
}
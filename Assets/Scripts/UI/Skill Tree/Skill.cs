using System.Collections.Generic;
using TMPro;
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

    public bool isFirstSkill = false;
    public bool completesBranch = false;

    public GameObject titleTMP;
    public GameObject descriptionTMP;
    public GameObject costTMP;

    Dictionary<string, TextMeshProUGUI> texts = new Dictionary<string, TextMeshProUGUI>();

    protected string skillName;
    protected string skillDesc; // ...ription

    private bool _unlockable;
    public bool Unlockable { get { return _unlockable; } set { _unlockable = value; UpdateButtonAppearance(); } }
    private bool _unlocked;
    public bool Unlocked { get { return _unlocked; } set { _unlocked = value; UpdateButtonAppearance(); } }

    protected MagesJSONParser.Mage mageJson;
    protected SkillManager SM;


    private Achievements achievements;
    private Laser laser;
    public void Start()
    {
        InitButton();
        InitStats();
        InitNextSkills();
        InitTexts();
        SM = SkillManager.Instance;
        UpdateButtonAppearance();
        achievements = GameObject.FindObjectsOfType<Achievements>()[0];
        laser = GameObject.FindObjectsOfType<Laser>()[0];
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
                    string level = gameObject.name.Substring(skillJson.id.Length);
                    cost = COSTS[level.Length];
                    skillName = skillJson.name;
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

    void InitTexts()
    {
        texts.Add("title", Instantiate(titleTMP, transform, false).GetComponent<TextMeshProUGUI>());
        texts.Add("description", Instantiate(descriptionTMP, transform, false).GetComponent<TextMeshProUGUI>());
        texts.Add("cost", Instantiate(costTMP, transform, false).GetComponent<TextMeshProUGUI>());

        texts["title"].text = skillName;
        texts["description"].text = skillDesc;
        texts["cost"].text = "Cost: " + cost + " SP";
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
            if (isFirstSkill)
            {
                SM.mageSpawner.SpawnMage(mageClass);
                achievements.onemageunlock = true;
            }
            if (this is FinalSkill)
            {

                achievements.finalskillunlock = true;
                laser.finalskillunlocked = true;
            }
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
        // Players can only update skills when no wave is ongoing
        button.interactable = GameStateManager.Instance.State == GameState.IDLE;
        // Disable buttons based on state of the button, to only show the next available button in each branch
        enabled = Unlockable && !Unlocked;
        GetComponent<Image>().enabled = enabled;
        // Match text enabled state to button, but keep text if its the final skill of the branch
        // and its unlocked, since the button disappears in that case, and we want to keep the text there
        if (completesBranch && Unlocked) texts["cost"].enabled = enabled;
        else foreach (TextMeshProUGUI tmp in texts.Values) tmp.enabled = enabled;
    }
}
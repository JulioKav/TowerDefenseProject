using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [HideInInspector] public List<Skill> nextSkills;

    [HideInInspector] public Button button;
    [HideInInspector] public MageClass mageClass;
    [HideInInspector] public int cost;

    public bool completesBranch = false;

    private bool _unlockable;
    public bool Unlockable { get { return _unlockable; } set { _unlockable = value; UpdateButtonAppearance(); } }
    private bool _unlocked;
    public bool Unlocked { get { return _unlocked; } set { _unlocked = value; UpdateButtonAppearance(); } }

    protected MagesJSONParser.Mage magesJson;
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
        magesJson = MagesJSONParser.Instance.magesJson.mages[(int)mageClass];
        foreach (var skillJson in magesJson.skills)
            if (skillJson.id == gameObject.name) cost = skillJson.cost;
        Unlockable = false;
        Unlocked = false;
    }

    void InitNextSkills()
    {
        nextSkills = new List<Skill>();
        foreach (Transform skillT in transform) nextSkills.Add(skillT.GetComponent<Skill>());
    }

    public void TryUnlockSkill()
    {
        if (SM.TryUnlockSkill(this))
        {
            Unlocked = true;
            if (completesBranch) FinalSkill.Instance.CheckUnlockable();
            foreach (Skill ns in nextSkills) ns.Unlockable = true;
        }
    }

    public void LockSkill()
    {
        foreach (Skill ns in nextSkills) ns.Unlockable = false;
        Unlocked = false;
    }

    void UpdateButtonAppearance()
    {
        // Udpate button based on if it is unlockable and/or unlocked
        button.interactable = Unlockable && !Unlocked;
        if (Unlocked)
        {
            var colors = button.colors;
            colors.disabledColor = new Color(0x52 / 255f, 0xE7 / 255f, 0x62 / 255f);
            button.colors = colors;
        }
        else
        {
            var colors = button.colors;
            colors.disabledColor = new Color(226 / 255f, 82 / 255f, 82 / 255f, 128 / 255f);
            button.colors = colors;
        }
    }
}
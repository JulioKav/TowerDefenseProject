using UnityEngine;

public class FinalSkill : Skill
{
    public static FinalSkill Instance;
    void Awake() { if (!Instance) Instance = this; }

    MagesJSONParser.Skill finalSkillJson;

    new public void Start()
    {
        base.Start();
        finalSkillJson = MagesJSONParser.Instance.magesJson.finalSkill;
        skillName = finalSkillJson.name;
        skillDesc = finalSkillJson.description;
        cost = finalSkillJson.cost;

    }

    public void CheckUnlockable()
    {
        foreach (bool bc in SkillManager.Instance.branchCompleted) if (!bc) return;
        Unlockable = true;
    }
}
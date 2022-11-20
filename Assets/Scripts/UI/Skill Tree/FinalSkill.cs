using UnityEngine;

public class FinalSkill : Skill
{
    public static FinalSkill Instance;
    void Awake() { if (!Instance) Instance = this; }

    public void CheckUnlockable()
    {
        foreach (bool bc in SkillManager.Instance.branchCompleted) if (!bc) return;
        Unlockable = true;
    }
}
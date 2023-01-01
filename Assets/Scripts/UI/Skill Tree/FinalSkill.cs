using UnityEngine;
using UnityEngine.UI;

public class FinalSkill : Skill
{
    public static FinalSkill Instance;
    void Awake() { if (!Instance) Instance = this; }

    MagesJSONParser.Skill finalSkillJson;

    public FinalSkillGraphic[] finalSkillGraphics;

    new public void Start()
    {
        base.Start();
        finalSkillJson = MagesJSONParser.Instance.magesJson.finalSkill;
        skillName = finalSkillJson.name;
        skillDesc = finalSkillJson.description;
        cost = FINAL_SKILL_COST;

    }

    public void CheckUnlockable()
    {
        bool unlockable = true;
        for (int i = 0; i < SkillManager.Instance.branchCompleted.Length; i++)
        {
            var bc = SkillManager.Instance.branchCompleted[i];
            Sprite s;
            switch (bc)
            {
                case 0:
                    s = finalSkillGraphics[i].notUnlocked;
                    unlockable = false;
                    break;
                case 1:
                    s = finalSkillGraphics[i].halfUnlocked;
                    unlockable = false;
                    break;
                default:
                    s = finalSkillGraphics[i].fullyUnlocked;
                    break;
            }
            finalSkillGraphics[i].GetComponent<Image>().sprite = s;
        }
        if (unlockable) Unlockable = true;
    }
}
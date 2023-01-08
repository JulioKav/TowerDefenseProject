using System.Collections.Generic;
using UnityEngine;

public abstract class Mage : MonoBehaviour
{

    // An interface for mage scripts

    private Dictionary<string, int> skillsUnlocked;
    StarDisplay starDisplay;
    public MageClass mageClass;

    public void Start()
    {
        starDisplay = GetComponentInChildren<StarDisplay>();
        skillsUnlocked = new Dictionary<string, int>();
    }

    public void OnEnable()
    {
        SkillManager.OnChangeSkill += ChangeSkillHandler;
    }

    public void OnDisable()
    {
        SkillManager.OnChangeSkill -= ChangeSkillHandler;
    }

    public virtual void ChangeSkillHandler(MageClass mageClass, string id, bool unlocked)
    {
        if (mageClass != this.mageClass) return;
        string name = id.TrimEnd('+');
        int level = id.Substring(name.Length).Length;
        skillsUnlocked[name] = level + (unlocked ? 0 : -1);
        Debug.Log(mageClass + " Mage - Skill " + (unlocked ? "Unlocked" : "Locked") + ": " +
                    name + " lvl " + (level + 1));
    }

    public int GetSkillLevel(string id)
    {
        return skillsUnlocked[id.TrimEnd('+')] + 1;
    }

    public void FillDictionary(string[] ids)
    {
        foreach (var id in ids) skillsUnlocked[id] = -1;
    }

}

using System.Collections.Generic;
using UnityEngine;

public abstract class Mage : MonoBehaviour
{

    // An interface for mage scripts

    private Dictionary<string, bool> skillsUnlocked;
    StarDisplay starDisplay;
    public MageClass mageClass;

    public void Start()
    {
        starDisplay = GetComponentInChildren<StarDisplay>();
        skillsUnlocked = new Dictionary<string, bool>();
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
        skillsUnlocked[id] = unlocked;
    }

    public bool IsSkillUnlocked(string id)
    {
        return (skillsUnlocked.ContainsKey(id) && skillsUnlocked[id]);
    }

}

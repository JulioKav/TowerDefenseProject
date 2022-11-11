using UnityEngine;

public abstract class Mage : MonoBehaviour
{

    // An interface for mage scripts

    bool[] skillsUnlocked;
    public StarDisplay starDisplay;

    // Start is called before the first frame update
    public void Start()
    {
        // by default they are not unlocked
        skillsUnlocked = new bool[] { false, false, false, false };
    }

    public virtual void UnlockSkill(int id)
    {
        // unlocks the skill and shows the star
        skillsUnlocked[id] = true;
        starDisplay.UnlockSkill(id);
    }

    public virtual void LockSkill(int id)
    {
        // locks unlocked skill and hides scar
        skillsUnlocked[id] = false;
        starDisplay.LockSkill(id);
    }

    public virtual void Skill1()
    {
        if (!skillsUnlocked[0]) return;
    }

    public virtual void Skill2()
    {
        if (!skillsUnlocked[1]) return;
    }

    public virtual void Skill3()
    {
        if (!skillsUnlocked[2]) return;
    }

    public virtual void Skill4()
    {
        if (!skillsUnlocked[3]) return;
    }

}

using UnityEngine;

public abstract class Mage : MonoBehaviour
{

    bool[] skillsUnlocked;
    public StarDisplay starDisplay;

    // Start is called before the first frame update
    public void Start()
    {
        skillsUnlocked = new bool[] { false, false, false, false };
    }

    public virtual void UnlockSkill(int id)
    {
        skillsUnlocked[id] = true;
        starDisplay.UnlockSkill(id);
    }

    public virtual void LockSkill(int id)
    {
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

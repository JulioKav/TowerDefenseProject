using UnityEngine;

public abstract class Mage : MonoBehaviour
{

    // An interface for mage scripts

    protected bool[] skillsUnlocked = new bool[] { false, false, false, false };
    StarDisplay starDisplay;

    void Awake()
    {
        starDisplay = GetComponentInChildren<StarDisplay>();
    }

    public virtual void UnlockSkill(int id)
    {
        // unlocks the skill and shows the star
        skillsUnlocked[id] = true;
        // starDisplay.UnlockSkill(id);
    }

    public virtual void LockSkill(int id)
    {
        // locks unlocked skill and hides scar
        skillsUnlocked[id] = false;
        starDisplay.LockSkill(id);
    }

}

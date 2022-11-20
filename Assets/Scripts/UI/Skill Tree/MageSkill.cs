using System;
using UnityEngine;

public class MageSkill : Skill
{
    // This class is for the initial button in the skill tree that unlocks the actual mage

    new public void Start()
    {
        InitMageClassesInBranch();
        base.Start();
        cost = magesJson.cost;
        Unlockable = true;
    }

    void InitMageClassesInBranch()
    {
        if (gameObject.name == "Bruiser") mageClass = MageClass.Physical;
        if (gameObject.name == "Wizard") mageClass = MageClass.Magic;
        if (gameObject.name == "Dreamer") mageClass = MageClass.Imaginary;
        if (gameObject.name == "Tinkerer") mageClass = MageClass.Mechanical;

        foreach (Skill skill in GetComponentsInChildren<Skill>()) skill.mageClass = mageClass;
    }

    public override void TryUnlockSkill()
    {
        base.TryUnlockSkill();
        if (Unlocked) SM.mageSpawner.SpawnMage(mageClass);
    }
}
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillManager : MonoBehaviour
{

    public static SkillManager Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    // Event for game end
    public delegate void GameEndEvent(int status); // 1 is win, 0 is loss
    public static event GameEndEvent OnGameEnd;

    public delegate void UnlockSkillEvent(MageClass mageClass, string id);
    public static event UnlockSkillEvent OnUnlockSkill;

    private LinkedList<Skill> unlockOrder;

    public MageSpawner mageSpawner;
    Mage[] mages;

    List<Skill> startingSkills;

    public int startingSkillPoints = 50;
    public int skillPoints { get; private set; }

    public TextMeshProUGUI endGameText;

    // Start is called before the first frame update
    void Start()
    {
        skillPoints = startingSkillPoints;
        unlockOrder = new LinkedList<Skill>();
        mages = new Mage[4];
        InitStartingSkills();
    }

    void InitStartingSkills()
    {
        startingSkills = new List<Skill>();
        foreach (Transform skillT in transform) if (skillT.tag == "MageSkill")
            {
                Skill skill = skillT.GetComponent<Skill>();
                startingSkills.Add(skill);
                skill.Unlockable = true;
            }
    }

    public bool TryUnlockSkill(Skill skill)
    {
        if (skill.cost <= skillPoints)
        {
            skillPoints -= skill.cost;
            unlockOrder.AddLast(skill);
            if (skill is MageSkill) mageSpawner.SpawnMage(skill.mageClass);
            if (OnUnlockSkill != null) OnUnlockSkill(skill.mageClass, skill.gameObject.name);
            return true;
        }
        return false;
    }

    public void AddSkillPoints(int skillPoints)
    {
        this.skillPoints += skillPoints;
    }

    public void SubtractSkillPoints(int skillPoints)
    {
        if (this.skillPoints >= skillPoints) this.skillPoints -= skillPoints;
        else
        // Losing these skill points makes player go below 0 skill points
        {
            if (unlockOrder.Count > 0)
            // There are any unlocked skills - lose the last bought skill
            {
                this.skillPoints += unlockOrder.Last.Value.cost / 2;
                this.skillPoints -= skillPoints;
                unlockOrder.Last.Value.LockSkill();
                unlockOrder.RemoveLast();
            }
            else
            // Lose the game
            {
                this.skillPoints = 0;
                if (OnGameEnd != null) OnGameEnd(0);
            }
        }
    }
}
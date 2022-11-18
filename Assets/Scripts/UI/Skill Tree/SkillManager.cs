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

    public delegate void ChangeSkillEvent(MageClass mageClass, string id, bool unlocked);
    public static event ChangeSkillEvent OnChangeSkill;

    private LinkedList<Skill> unlockOrder;

    public MageSpawner mageSpawner;

    public int startingSkillPoints = 50;
    public int skillPoints { get; private set; }

    [HideInInspector] public bool[] branchCompleted;

    // Start is called before the first frame update
    void Start()
    {
        skillPoints = startingSkillPoints;
        unlockOrder = new LinkedList<Skill>();
        branchCompleted = new bool[] { false, false, false, false };
    }

    public bool TryUnlockSkill(Skill skill)
    {
        if (skill.cost <= skillPoints)
        {
            skillPoints -= skill.cost;
            unlockOrder.AddLast(skill);
            if (skill.completesBranch) branchCompleted[(int)skill.mageClass] = true;
            // Broadcast Event
            if (OnChangeSkill != null) OnChangeSkill(skill.mageClass, skill.gameObject.name, true);
            // Check for Game End
            if (skill is FinalSkill && OnGameEnd != null) OnGameEnd(1);
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
            // There are unlocked skills - lose last bought skill
            {
                Skill lastUnlocked = unlockOrder.Last.Value;
                // Update Skill Points
                this.skillPoints += lastUnlocked.cost / 2;
                this.skillPoints -= skillPoints;
                // Lock the skill
                if (lastUnlocked is MageSkill) mageSpawner.DespawnMage(lastUnlocked.mageClass);
                if (lastUnlocked.completesBranch) branchCompleted[(int)lastUnlocked.mageClass] = false;
                lastUnlocked.LockSkill();
                if (OnChangeSkill != null) OnChangeSkill(lastUnlocked.mageClass, lastUnlocked.gameObject.name, false);
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
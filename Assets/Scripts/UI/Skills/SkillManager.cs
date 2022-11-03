using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    #region Structures & Helpers
    enum SkillClass
    {
        PHYSICAL, MAGIC, IMAGINARY, MECHANIC, FINAL
    }

    struct Skill
    {
        public SkillClass skillClass;
        public int skillIdx;
        public Button button;
        public bool unlockable;
        public bool unlocked;
        public int cost;

        public Skill(SkillClass skillClass, int skillIdx, bool unlockable, Button button, int cost)
        {
            this.skillClass = skillClass;
            this.skillIdx = skillIdx;
            this.button = button;
            this.unlockable = unlockable;
            unlocked = false;
            this.cost = cost;
        }
    }

    struct SkillBranch
    {

        SkillClass skillClass;
        public Skill[] skills;
        public bool completed;

        public SkillBranch(SkillClass skillClass)
        {
            this.skillClass = skillClass;
            skills = new Skill[5];
            completed = false;
        }
    }
    #endregion

    SkillBranch[] branches;
    Skill finalSkill;
    public Button finalSkillBtn;

    private LinkedList<Skill> unlockOrder;

    public int skillPoints = 50;

    const int BASE_COST = 50;
    const int COST_PER_LEVEL = 50;
    const int FINAL_SKILL_COST = 500;

    public TextMeshProUGUI endGameText;

    // Start is called before the first frame update
    void Start()
    {
        InitBranches();
        unlockOrder = new LinkedList<Skill>();
    }

    void InitBranches()
    {
        branches = new SkillBranch[4];
        for (int i = 0; i < branches.Length; i++)
            branches[i] = new SkillBranch((SkillClass)i);


        foreach (Transform branchT in transform)
        {
            if (branchT.tag != "SkillBranch") continue;
            int classIdx = -1;
            switch (branchT.name)
            {
                case "Physical":
                    classIdx = (int)SkillClass.PHYSICAL;
                    break;
                case "Magic":
                    classIdx = (int)SkillClass.MAGIC;
                    break;
                case "Imaginary":
                    classIdx = (int)SkillClass.IMAGINARY;
                    break;
                case "Mechanic":
                    classIdx = (int)SkillClass.MECHANIC;
                    break;
                default:
                    break;
            }
            int cost = BASE_COST;
            foreach (Transform skillT in branchT)
            {
                if (skillT.tag != "Skill") continue;
                int skillIdx = int.Parse(skillT.name.Substring(5)) - 1;
                Button skillBtn = skillT.GetComponent<Button>();
                branches[classIdx].skills[skillIdx] = new Skill((SkillClass)classIdx, skillIdx, skillIdx == 0, skillBtn, cost);
                branches[classIdx].skills[skillIdx].button.onClick.AddListener(() => TryUnlockSkill(branches[classIdx].skills[skillIdx]));
                // TODO: This is prototype code, remove later
                if (classIdx > 0 && skillIdx == 0)
                {
                    branches[classIdx].completed = true;
                    branches[classIdx].skills[skillIdx].unlockable = false;
                }
                // TODO This is normal code again, dont delete
                UpdateButtonAppearance(branches[classIdx].skills[skillIdx]);
                cost += COST_PER_LEVEL;
            }
        }

        finalSkill = new Skill(SkillClass.FINAL, -1, false, finalSkillBtn, FINAL_SKILL_COST);
        finalSkill.button.onClick.AddListener(() => TryUnlockSkill(finalSkill));
    }

    void TryUnlockSkill(Skill skill)
    {
        // TODO: check if enough Skill Points are available to unlock skill
        if (skillPoints >= skill.cost)
        {
            skillPoints -= skill.cost;
            // Unlock skill
            skill.unlocked = true;
            unlockOrder.AddLast(skill);
            UpdateButtonAppearance(skill);
            // Special treatment for final Skill
            if (skill.skillClass == SkillClass.FINAL)
            {
                endGameText.text = "YOU WIN!";
                return;
            }
            // See if that makes the next skill unlockable
            SkillBranch branch = branches[(int)skill.skillClass];
            int nextSkillIdx = skill.skillIdx + 1;
            if (nextSkillIdx == branch.skills.Length)
            {
                // If a branch is completed, mark it as such and check if all branches are completed
                branches[(int)skill.skillClass].completed = true;
                CheckForFinalSkill();
            }
            else
            {
                Skill nextSkill = branch.skills[nextSkillIdx];
                nextSkill.unlockable = true;
                UpdateButtonAppearance(nextSkill);
            }
        }
    }

    void LockSkill(Skill skill)
    {
        int bId = (int)skill.skillClass;
        branches[bId].completed = false;
        CheckForFinalSkill();
        skill.unlocked = false;
        skill.unlockable = true;
        UpdateButtonAppearance(skill);
        int sId = skill.skillIdx;
        if (sId < branches[bId].skills.Length - 1)
        {
            Skill nextSkill = branches[bId].skills[sId + 1];
            nextSkill.unlockable = false;
            UpdateButtonAppearance(nextSkill);
        }
    }

    public void AddSkillPoints(int skillPoints)
    {
        this.skillPoints += skillPoints;
    }

    public void SubtractSkillPoints(int skillPoints)
    {
        if (this.skillPoints >= skillPoints)
        {
            this.skillPoints -= skillPoints;
        }
        else
        {
            if (unlockOrder.Count > 0)
            {
                this.skillPoints += unlockOrder.Last.Value.cost / 2;
                this.skillPoints -= skillPoints;
                LockSkill(unlockOrder.Last.Value);
                unlockOrder.RemoveLast();
            }
            else
            {
                this.skillPoints = 0;
                endGameText.text = "YOU LOSE!";
                // TODO: Lose game
            }
        }
    }

    void UpdateButtonAppearance(Skill skill)
    {
        skill.button.interactable = skill.unlockable && !skill.unlocked;
        if (skill.unlocked)
        {
            var colors = skill.button.colors;
            colors.disabledColor = new Color(0x52 / 255f, 0xE7 / 255f, 0x62 / 255f);
            skill.button.colors = colors;
        }
        else
        {
            var colors = skill.button.colors;
            colors.disabledColor = new Color(226 / 255f, 82 / 255f, 82 / 255f, 128 / 255f);
            skill.button.colors = colors;
        }
    }

    void CheckForFinalSkill()
    {
        bool allBranchesCompleted = true;
        foreach (var branch in branches) if (!branch.completed) allBranchesCompleted = false;
        finalSkill.unlockable = allBranchesCompleted;
        UpdateButtonAppearance(finalSkill);
    }
}
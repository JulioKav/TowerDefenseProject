using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    #region Structures & Helpers
    public enum SkillClass
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

    // Event for game end
    public delegate void GameEndEvent(int status); // 1 is win, 0 is loss
    public static event GameEndEvent OnGameEnd;

    SkillBranch[] branches;
    Skill finalSkill;
    public Button finalSkillBtn;

    private LinkedList<Skill> unlockOrder;

    public MageSpawner mageSpawner;
    Mage[] mages;

    public int skillPoints = 5000;

    const int BASE_COST = 50;
    const int COST_PER_LEVEL = 150;
    const int FINAL_SKILL_COST = 1000;

    public TextMeshProUGUI endGameText;

    // Start is called before the first frame update
    void Start()
    {
        InitBranches();
        unlockOrder = new LinkedList<Skill>();
        mages = new Mage[4];
    }

    void InitBranches()
    {
        branches = new SkillBranch[4];
        for (int i = 0; i < branches.Length; i++)
            branches[i] = new SkillBranch((SkillClass)i);

        // Initialize branches to have default values
        foreach (Transform branchT in transform)
        {
            // Get children with tag "SkillBranch"
            if (branchT.tag != "SkillBranch") continue;
            int bId = -1;
            switch (branchT.name)
            {
                case "Physical":
                    bId = (int)SkillClass.PHYSICAL;
                    break;
                case "Magic":
                    bId = (int)SkillClass.MAGIC;
                    break;
                case "Imaginary":
                    bId = (int)SkillClass.IMAGINARY;
                    break;
                case "Mechanic":
                    bId = (int)SkillClass.MECHANIC;
                    break;
                default:
                    break;
            }
            int cost = BASE_COST;
            foreach (Transform skillT in branchT)
            {
                // Initialize each skill in the branch
                if (skillT.tag != "Skill") continue;
                // get skill index
                int sId = int.Parse(skillT.name.Substring(5)) - 1;
                // only first skill is unlockable at start
                bool unlockable = sId == 0;
                // Add the respective button to the skill, along with an onclick function, then update button text
                Button skillBtn = skillT.GetComponent<Button>();
                branches[bId].skills[sId] = new Skill((SkillClass)bId, sId, unlockable, skillBtn, cost);
                branches[bId].skills[sId].button.onClick.AddListener(() => TryUnlockSkill(branches[bId].skills[sId]));
                skillBtn.GetComponentInChildren<TextMeshProUGUI>().text += "\n" + cost + "SP";
                // TODO: This is prototype code to disable other 3 branches, remove later
                if (bId > 0 && sId == 0)
                {
                    branches[bId].completed = true;
                    branches[bId].skills[sId].unlockable = false;
                }
                // TODO END This is normal code again, dont delete
                // Updates button based on unlockable and unlocked bools
                UpdateButtonAppearance(branches[bId].skills[sId]);
                cost += COST_PER_LEVEL;
            }
        }

        // Do same for final skill
        finalSkill = new Skill(SkillClass.FINAL, -1, false, finalSkillBtn, FINAL_SKILL_COST);
        finalSkill.button.onClick.AddListener(() => TryUnlockSkill(finalSkill));
        finalSkill.button.GetComponentInChildren<TextMeshProUGUI>().text += "\n" + FINAL_SKILL_COST + "SP";
    }

    void UnlockMage(SkillClass skillClass)
    {
        // When skill 0 is bought
        mages[(int)skillClass] = mageSpawner.SpawnMage(skillClass).GetComponent<Mage>();
    }

    void LockMage(SkillClass skillClass)
    {
        // When skill 0 is lost
        mageSpawner.DespawnMage(skillClass);
        mages[(int)skillClass] = null;
    }

    void TryUnlockSkill(Skill skill)
    {
        // check if enough Skill Points are available to unlock skill
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
                OnGameEnd(1);
                return;
            }
            // Spawn mage if first skill
            if (skill.skillIdx == 0) UnlockMage(skill.skillClass);
            else mages[(int)skill.skillClass].UnlockSkill(skill.skillIdx - 1);
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
                // if there isnother skill, make it unlockable
                Skill nextSkill = branch.skills[nextSkillIdx];
                nextSkill.unlockable = true;
                UpdateButtonAppearance(nextSkill);
            }
        }
    }

    // Lock a previously unlocked skill
    void LockSkill(Skill skill)
    {
        int bId = (int)skill.skillClass;
        // Mark branch as uncompleted, then update final skill and lock it again if necessary
        branches[bId].completed = false;
        CheckForFinalSkill();
        // Update skills status and appearance
        skill.unlocked = false;
        skill.unlockable = true;
        UpdateButtonAppearance(skill);
        // Despawn mage if it was first skill, otherwise remove the respective skill
        if (skill.skillIdx == 0) LockMage(skill.skillClass);
        else mages[(int)skill.skillClass].LockSkill(skill.skillIdx - 1);
        int sId = skill.skillIdx;
        // Update unlockable status of next skill, where relevant
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
        // if losing these skill points makes player go below 0 skill points
        {
            if (unlockOrder.Count > 0)
            // If there are any unlocked skills, lose the last bought skill
            {
                this.skillPoints += unlockOrder.Last.Value.cost / 2;
                this.skillPoints -= skillPoints;
                LockSkill(unlockOrder.Last.Value);
                unlockOrder.RemoveLast();
            }
            else
            // Lose the game
            {
                this.skillPoints = 0;
                endGameText.text = "YOU LOSE!";
                if (OnGameEnd != null) OnGameEnd(0);
            }
        }
    }

    void UpdateButtonAppearance(Skill skill)
    {
        // Udpate button based on if it is unlockable and/or unlocked
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
        // If all 4 branches are complete, allow final skill to be unlockable
        bool allBranchesCompleted = true;
        foreach (var branch in branches) if (!branch.completed) allBranchesCompleted = false;
        finalSkill.unlockable = allBranchesCompleted;
        UpdateButtonAppearance(finalSkill);
    }
}
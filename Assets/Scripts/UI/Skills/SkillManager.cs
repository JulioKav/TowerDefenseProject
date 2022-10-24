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

        public Skill(SkillClass skillClass, int skillIdx, bool unlockable, Button button)
        {
            this.skillClass = skillClass;
            this.skillIdx = skillIdx;
            this.button = button;
            this.unlockable = unlockable;
            unlocked = false;
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

    // Start is called before the first frame update
    void Start()
    {
        InitBranches();
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
            foreach (Transform skillT in branchT)
            {
                if (skillT.tag != "Skill") continue;
                int skillIdx = int.Parse(skillT.name.Substring(5)) - 1;
                Button skillBtn = skillT.GetComponent<Button>();
                branches[classIdx].skills[skillIdx] = new Skill((SkillClass)classIdx, skillIdx, skillIdx == 0, skillBtn);
                branches[classIdx].skills[skillIdx].button.onClick.AddListener(() => TryUnlockSkill(branches[classIdx].skills[skillIdx]));
                UpdateButtonAppearance(branches[classIdx].skills[skillIdx]);
            }
        }

        finalSkill = new Skill(SkillClass.FINAL, -1, false, finalSkillBtn);
        finalSkill.button.onClick.AddListener(() => TryUnlockSkill(finalSkill));
    }

    void TryUnlockSkill(Skill skill)
    {
        // TODO: check if enough Skill Points are available to unlock skill
        skill.unlocked = true;
        UpdateButtonAppearance(skill);
        // Special treatment for final Skill
        if (skill.skillClass == SkillClass.FINAL) return;
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

    void UpdateButtonAppearance(Skill skill)
    {
        skill.button.interactable = skill.unlockable && !skill.unlocked;
        if (skill.unlocked)
        {
            var colors = skill.button.colors;
            colors.disabledColor = Color.green;
            skill.button.colors = colors;
        }
    }

    void CheckForFinalSkill()
    {
        foreach (var branch in branches) if (!branch.completed) return;
        finalSkill.unlockable = true;
        UpdateButtonAppearance(finalSkill);
    }
}
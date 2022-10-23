using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    #region Structures & Helpers
    enum SkillClass
    {
        PHYSICAL, MAGIC, IMAGINARY, MECHANIC
    }

    struct Skill
    {
        public bool unlockable;
        public bool unlocked;
        public Button button;

        public Skill(bool unlockable)
        {
            this.unlockable = unlockable;
            unlocked = false;
            button = null;
        }
    }

    struct SkillBranch
    {

        SkillClass skillClass;
        public Skill[] skills;
        public GameObject branchGO;

        public SkillBranch(SkillClass skillClass)
        {
            this.skillClass = skillClass;
            branchGO = null;
            skills = new Skill[5];
            for (int i = 0; i < skills.Length; i++)
            {
                skills[i] = new Skill(i == 0);
            }
        }
    }
    #endregion

    SkillBranch[] branches;
    Skill finalSkill;

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

        foreach (Transform child in transform)
        {
            if (child.tag == "SkillBranch")
                switch (child.name)
                {
                    case "Physical":
                        branches[(int)SkillClass.PHYSICAL].branchGO = child.gameObject;
                        break;
                    case "Magic":
                        branches[(int)SkillClass.MAGIC].branchGO = child.gameObject;
                        break;
                    case "Imaginary":
                        branches[(int)SkillClass.IMAGINARY].branchGO = child.gameObject;
                        break;
                    case "Mechanic":
                        branches[(int)SkillClass.MECHANIC].branchGO = child.gameObject;
                        break;
                    default:
                        break;
                }
        }
    }
}

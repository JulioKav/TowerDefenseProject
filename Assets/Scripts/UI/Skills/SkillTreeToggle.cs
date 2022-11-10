using TMPro;
using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public Transform skillTree;
    private Vector3 skillTreePos;

    // Game objects to hide when skill tree is open
    public Transform hideWhenSkillTreeVisible;
    private Vector3 hideWhenSkillTreeVisiblePos;

    public GameObject background;

    private TextMeshProUGUI text;

    bool skillTreeVisible;

    // Start is called before the first frame update
    void Start()
    {
        skillTreePos = skillTree.position;
        hideWhenSkillTreeVisiblePos = hideWhenSkillTreeVisible.position;
        skillTreeVisible = false;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        HideSkillTree();
    }

    public void ToggleSkillTree()
    {
        skillTreeVisible = !skillTreeVisible;
        if (skillTreeVisible) ShowSkillTree();
        else HideSkillTree();
    }

    private void HideSkillTree()
    {
        skillTree.position += Vector3.right * 10000;
        hideWhenSkillTreeVisible.position = hideWhenSkillTreeVisiblePos;
        background.SetActive(false);
        text.text = "Show Skill Tree";
    }

    private void ShowSkillTree()
    {
        skillTree.position = skillTreePos;
        hideWhenSkillTreeVisible.position += Vector3.right * 10000;
        background.SetActive(true);
        text.text = "Hide Skill Tree";
    }

}

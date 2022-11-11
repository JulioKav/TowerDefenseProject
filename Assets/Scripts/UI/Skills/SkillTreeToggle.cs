using TMPro;
using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public Transform skillTree;
    private Vector3 skillTreePos;

    // Game objects to hide when skill tree is open
    public Transform hideWhenSkillTreeVisible;
    private Vector3 hideWhenSkillTreeVisiblePos;

    // Background that shows when skill tree is open
    public GameObject background;

    // Skill Tree Toggle button text
    private TextMeshProUGUI text;

    bool skillTreeVisible;

    // Start is called before the first frame update
    void Start()
    {
        // Get old positions of UI elements
        skillTreePos = skillTree.position;
        hideWhenSkillTreeVisiblePos = hideWhenSkillTreeVisible.position;
        // Set default values
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
        // Hides skill tree by moving it off screen, and moving other UI elements back on screen.
        // Cant disable due to lost functionality
        skillTree.position += Vector3.right * 10000;
        hideWhenSkillTreeVisible.position = hideWhenSkillTreeVisiblePos;
        background.SetActive(false);
        text.text = "Show Skill Tree";
    }

    private void ShowSkillTree()
    {
        // Moves UI elements back on/off screen respectively.
        skillTree.position = skillTreePos;
        hideWhenSkillTreeVisible.position += Vector3.right * 10000;
        background.SetActive(true);
        text.text = "Hide Skill Tree";
    }

}

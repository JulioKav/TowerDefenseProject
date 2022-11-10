using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public GameObject skillTree;
    private Vector3 skillTreePos;

    // Game objects to hide when skill tree is open
    public GameObject inventory;
    public GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        skillTreePos = skillTree.transform.position;
        HideSkillTree();
    }

    public void ToggleSkillTree()
    {
        bool enabled = background.activeSelf;
        if (enabled) HideSkillTree();
        else ShowSkillTree();
        // toggle other UI elements
        background.SetActive(!enabled);
        inventory.SetActive(enabled);
    }

    private void HideSkillTree()
    {
        skillTree.transform.position = Vector3.right * 10000;
    }

    private void ShowSkillTree()
    {
        skillTree.transform.position = skillTreePos;
    }

}

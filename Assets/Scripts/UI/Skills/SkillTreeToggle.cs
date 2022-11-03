using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public GameObject skillTree;

    // Game objects to hide when skill tree is open
    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        skillTree.SetActive(false);
    }

    public void ToggleSkillTree()
    {
        bool enabled = skillTree.activeSelf;
        skillTree.SetActive(!enabled);
        // toggle other UI elements
        inventory.SetActive(enabled);
    }

}

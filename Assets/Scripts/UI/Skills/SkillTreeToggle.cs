using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public GameObject skillTree;
    // Start is called before the first frame update
    void Start()
    {
        skillTree.SetActive(false);
    }

    public void ToggleSkillTree() {
        skillTree.SetActive(!skillTree.activeSelf);
    }
}

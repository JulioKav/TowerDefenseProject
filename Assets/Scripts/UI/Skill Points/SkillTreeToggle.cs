using TMPro;
using UnityEngine;

public class SkillTreeToggle : MonoBehaviour
{
    public delegate void ToggleSkillTreeEvent();
    public static event ToggleSkillTreeEvent OnToggleSkillTree;

    // Skill Tree Toggle button text
    private TextMeshProUGUI text;

    bool skillTreeVisible;

    // Start is called before the first frame update
    void Awake()
    {
        // Set default values
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleSkillTree()
    {
        if (OnToggleSkillTree != null) OnToggleSkillTree();
    }

    void OnEnable()
    {
        UIStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        UIStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(UIState newState)
    {
        if (newState == UIState.DEFAULT) HideSkillTree();
        if (newState == UIState.SKILL_TREE) ShowSkillTree();
    }

    private void HideSkillTree()
    {
        text.text = "Show Skill Tree";
    }

    private void ShowSkillTree()
    {
        text.text = "Hide Skill Tree";
    }

}

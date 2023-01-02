using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeToggle : MonoBehaviour
{
    // Skill Tree Toggle button text
    private TextMeshProUGUI text;

    bool skillTreeVisible;

    // Start is called before the first frame update
    void Awake()
    {
        // Set default values
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UIStateManager.Instance.ToggleSkillTree);
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
        if (newState == UIState.INVENTORY) HideSkillTree();
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

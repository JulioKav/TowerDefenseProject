using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance;
    void Awake() { if (!Instance) Instance = this; }

    private UIState _state;
    public UIState State { get { return _state; } private set { _state = value; if (OnStateChange != null) OnStateChange(_state); } }

    public delegate void StateChangeEvent(UIState newState);
    public static event StateChangeEvent OnStateChange;

    // Start is called before the first frame update
    void Start()
    {
        State = UIState.DEFAULT;
    }

    void OnEnable()
    {
        SkillTreeToggle.OnToggleSkillTree += ToggleSkillTreeHandler;
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        SkillTreeToggle.OnToggleSkillTree -= ToggleSkillTreeHandler;
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void ToggleSkillTreeHandler()
    {
        if (State == UIState.DEFAULT) State = UIState.SKILL_TREE;
        else State = UIState.DEFAULT;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.IDLE:
                State = UIState.DEFAULT;
                break;
            default:
                break;
        }
    }
}

public enum UIState
{
    DEFAULT,
    SKILL_TREE,
    BOONS
}

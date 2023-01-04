using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    // Singleton so any class can trigger functions here, like toggling the skill tree
    public static UIStateManager Instance;
    void Awake() { if (!Instance) Instance = this; }

    // Event when state is changed, including the new state
    public delegate void StateChangeEvent(UIState newState);
    public static event StateChangeEvent OnStateChange;

    // State property that, when set, automatically triggers the OnStateChange event
    private UIState _state = UIState.DIALOGUE;
    public UIState State
    {
        get
        { return _state; }
        private set
        {
            Debug.Log("UI State: " + _state + " -> " + value);
            var oldState = _state;
            _state = value;
            if (OnStateChange != null && oldState != value) OnStateChange(value);
        }
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    public void ToggleSkillTree()
    {
        if (State == UIState.INVENTORY) State = UIState.SKILL_TREE;
        else State = UIState.INVENTORY;
    }

    public void BoonSelected()
    {
        if (State != UIState.BOONS) return;
        State = UIState.INVENTORY;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.PRE_GAME:
            case GameState.PRE_ROUND_DIALOGUE:
                State = UIState.DIALOGUE;
                break;
            case GameState.POST_ROUND:
                State = UIState.DEFAULT;
                break;
            case GameState.ROUND_ONGOING:
                if (State == UIState.DIALOGUE) State = UIState.INVENTORY;
                break;
            case GameState.IDLE:
                State = UIState.BOONS;
                break;
            default:
                break;
        }
    }
}

public enum UIState
{
    DEFAULT,        // Nothing interactable
    INVENTORY,      // Inventory + next wave button
    SKILL_TREE,     // Skill tree
    BOONS,          // End of wave boon selector screen
    DIALOGUE,       // When dialogue is happening
}

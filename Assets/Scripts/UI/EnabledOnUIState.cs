using UnityEngine;

public class EnabledOnUIState : MonoBehaviour
{

    public UIState[] enabledStates;
    public bool enabledAtStart = true;

    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = enabledAtStart;
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
        canvas.enabled = false;
        foreach (UIState es in enabledStates) if (es == newState) canvas.enabled = true;
    }

}
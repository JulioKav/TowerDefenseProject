using UnityEngine;

public class EnabledOnUIState : MonoBehaviour
{

    public UIState[] enabledStates;

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
        Canvas canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        foreach (UIState es in enabledStates) if (es == newState) canvas.enabled = true;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Singleton so any class can trigger functions here like ending the round
    public static GameStateManager Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    // Event when state is changed, including the new state
    public delegate void StateChangeEvent(GameState newState);
    public static event StateChangeEvent OnStateChange;

    // State property that, when set, automatically triggers the OnStateChange event
    private GameState _state;
    public GameState State
    {
        get { return _state; }
        set
        {
            Debug.Log(_state + ", " + value);
            if (OnStateChange != null && _state != value)
                OnStateChange(value); _state = value;
        }
    }


    public int PreRoundTimeInSeconds = 3;
    public int PostRoundTimeInSeconds = 2;

    void Start()
    {
        State = GameState.IDLE;
    }

    public void StartRound()
    {
        if (State != GameState.IDLE) return;
        State = GameState.PRE_ROUND;
        StartCoroutine(StartPreRound());
    }

    public void EndRound()
    {
        if (State != GameState.ROUND_ONGOING) return;
        State = GameState.POST_ROUND;
        StartCoroutine(StartPostRound());
    }

    private IEnumerator StartPreRound()
    {
        Debug.Log("Pre Round");
        yield return new WaitForSeconds(PreRoundTimeInSeconds);
        Debug.Log("Round Round");
        State = GameState.ROUND_ONGOING;
    }

    private IEnumerator StartPostRound()
    {
        Debug.Log("Post Round");
        yield return new WaitForSeconds(PostRoundTimeInSeconds);
        Debug.Log("Idle");
        State = GameState.IDLE;
    }
}

public enum GameState
{
    PRE_ROUND,          // Delay before wave spawns after pressing next wave button 
    ROUND_ONGOING,      // Wave is currently active
    POST_ROUND,         // Delay after wave is defeated, before game continues
    IDLE,               // State where player places turrets, upgrades skill tree, etc.
    WIN,                // Player won
    LOSE                // Player lost
}
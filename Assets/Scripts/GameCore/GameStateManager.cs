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
    private GameState _state = GameState.PRE_GAME;
    public GameState State
    {
        get { return _state; }
        set
        {
            Debug.Log("Game State: " + _state + " -> " + value);
            var oldState = _state;
            _state = value;
            if (OnStateChange != null && oldState != value) OnStateChange(value);
        }
    }

    public int PreRoundTimeInSeconds = 3;
    public int PostRoundTimeInSeconds = 2;

    int newSpawnPointEveryXWaves = 5;

    public void StartRound()
    {
        if (State != GameState.IDLE) return;
        StartPreRound();
    }

    public void EndDialogue()
    {
        if (State == GameState.PRE_GAME) StartPostRound();
        if (State == GameState.PRE_ROUND_DIALOGUE) State = GameState.ROUND_ONGOING;
    }

    public void EndRound()
    {
        if (State != GameState.ROUND_ONGOING) return;
        StartPostRound();
    }

    public void EndGame(bool victory)
    {
        if (victory) State = GameState.WIN;
        else State = GameState.LOSE;
    }

    public void PathGenerationDone()
    {
        if (State == GameState.PATH_GENERATION) State = GameState.IDLE;
    }

    private void StartPreRound()
    {
        State = GameState.PRE_ROUND;
        StartCoroutine(CountdownPreRound());
    }

    private void StartPreRoundDialogue()
    {
        if (Random.Range(0f, 1f) < .67f) State = GameState.ROUND_ONGOING;
        else State = GameState.PRE_ROUND_DIALOGUE;
    }

    private void StartPostRound()
    {
        State = GameState.POST_ROUND;
        StartCoroutine(CountdownPostRound());
    }

    private IEnumerator CountdownPreRound()
    {
        yield return new WaitForSeconds(PreRoundTimeInSeconds);
        StartPreRoundDialogue();
    }

    private IEnumerator CountdownPostRound()
    {
        yield return new WaitForSeconds(PostRoundTimeInSeconds);
        bool generateNewPath = WaveSpawner.WaveNumber % newSpawnPointEveryXWaves == 0 &&
                               !PathGenerator.Instance.allSpawnPointsActive();
        State = (generateNewPath) ? GameState.PATH_GENERATION : GameState.IDLE;
    }
}

public enum GameState
{
    PRE_GAME,           // Start of game for dialogue
    PRE_ROUND,          // Delay before wave spawns after pressing next wave button 
    PRE_ROUND_DIALOGUE, // [Optional] One-line dialogue before the start of the wave
    ROUND_ONGOING,      // Wave is currently active
    POST_ROUND,         // Delay after wave is defeated, before game continues
    PATH_GENERATION,    // [Optional] Generates a new path on the terrain
    IDLE,               // State where player places turrets, upgrades skill tree, etc.
    WIN,                // Player won
    LOSE                // Player lost
}
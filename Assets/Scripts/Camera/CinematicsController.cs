using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicsController : MonoBehaviour
{
    public static CinematicsController Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    Vector3 lastPos;
    Queue<Vector3> positions;

    bool pausedOnObject = true;

    float timePerPositionInSeconds = 2f;

    public float preGameTransitionTimeInSeconds = 2f;
    public float pathGenTransitionTimeInSeconds = 3f;

    void Start()
    {
        positions = new Queue<Vector3>();
        StartPreGameCinematic();
        StartCoroutine(Unpause());
    }

    void Update()
    {
        if (positions.Count == 0) return;
        if (pausedOnObject) return;

        Vector3 target = positions.Peek();
        float distance = Vector3.Distance(lastPos, target) * Time.deltaTime / timePerPositionInSeconds;
        transform.position = Vector3.MoveTowards(transform.position, target, distance);
        if (transform.position == target)
        {
            lastPos = positions.Dequeue();
            pausedOnObject = true;
            StartCoroutine(Unpause());
        }
    }

    void StartPreGameCinematic()
    {
        SkipCinematic();
        timePerPositionInSeconds = preGameTransitionTimeInSeconds;
        lastPos = transform.position;
        foreach (var sp in GameObject.FindGameObjectsWithTag("SpawnPoint")) positions.Enqueue(sp.transform.position);
        positions.Enqueue(Vector3.zero);
    }

    public void SetupPathGenerationCinematic()
    {
        SkipCinematic();
        timePerPositionInSeconds = 1f;
        lastPos = transform.position;
        positions.Enqueue(PathGenerator.Instance.inactiveSpawnPoints.Peek().position);
    }

    public void StartPathGenerationCinematic()
    {
        SkipCinematic();
        timePerPositionInSeconds = pathGenTransitionTimeInSeconds;
        lastPos = transform.position;
        positions.Enqueue(Vector3.zero);
    }

    void SkipCinematic()
    {
        if (positions.Count == 0) return;
        Vector3 finalPos = positions.Dequeue();
        while (positions.Count > 0) finalPos = positions.Dequeue();
        transform.position = finalPos;
    }

    IEnumerator Unpause()
    {
        yield return new WaitForSeconds(0.25f);
        pausedOnObject = false;
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        GameState oldState = GameStateManager.Instance.State;
        if (oldState == GameState.PRE_GAME && newState == GameState.POST_ROUND) SkipCinematic();
        if (newState == GameState.PATH_GENERATION) SetupPathGenerationCinematic();
    }
}

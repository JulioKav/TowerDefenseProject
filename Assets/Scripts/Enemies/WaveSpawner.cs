using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    GameObject[] spawnPoints;
    // The game object enemies will be attached to
    public Transform enemiesParent;

    public float individualSpawnDelay = 0.5f;

    // Variables for wave management
    public List<Transform>[] waves;
    int currentWaveId;
    Transform[] currentWave;
    [HideInInspector]
    public bool waveOnGoing;

    // For emitting a round end event for other things to subscribe to
    public delegate void RoundEndEvent();
    public static event RoundEndEvent OnRoundEnd;

    void Start()
    {
        waves = new List<Transform>[4];
        PopulateWave();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        waveOnGoing = false;
        currentWave = null;
    }

    private void Update()
    {
    }

    // Populates the wave with enemy prefabs
    void PopulateWave()
    {
        // Wave 1-4
        for (int wave = 0; wave < 4; wave++)
        {
            // Adds 4, then 8, then 16, then 32 enemies in the waves 1-4, respectively
            waves[wave] = new List<Transform>();
            for (int i = 0; i < Mathf.Pow(2, wave + 2); i++)
            {
                if (i % 2 == 0)
                {
                    waves[wave].Add(enemyPrefab[0]);
                }
                else
                {
                    waves[wave].Add(enemyPrefab[1]);
                }
            }
        }

    }

    public void ButtonInput()
    {
        // Spawns waves depending on wave index
        if (currentWave != null) return;
        switch (currentWaveId)
        {
            // Handles wave logic based on wave number
            case 0:
                StartCoroutine(StartWave(3));
                break;
            case 1:
                StartCoroutine(StartWave(3));
                break;
            case 2:
                StartCoroutine(StartWave(2));
                break;
            case 3:
                StartCoroutine(StartWave(2));
                break;
            // after wave index 3, just spawn this double wave
            default:
                StartCoroutine(StartWave(2));
                StartCoroutine(StartWave(3));
                break;

        }
        StartCoroutine(CheckWaveComplete());
        currentWaveId++;
    }

    // Starts spawning a wave in a coroutine with a delay between enemies
    public IEnumerator StartWave(int spawnPointId)
    {
        // After wave 3, spwans the same wave over and over
        int waveId = (currentWaveId > 3) ? 3 : currentWaveId;
        currentWave = new Transform[waves[waveId].Count];
        for (int i = 0; i < waves[waveId].Count; i++)
        {
            var enemy = SpawnEnemy(waves[waveId][i].gameObject, spawnPointId);
            currentWave[i] = enemy.transform;
            yield return new WaitForSeconds(individualSpawnDelay);
        }
    }

    // Spawn an enemy game object at the spawnPoint, and return the instance
    GameObject SpawnEnemy(GameObject enemyGO, int spawnPointId)
    {
        // Create enemy objects at designated spawnpoints 
        var enemy = Instantiate(enemyGO,
                                spawnPoints[spawnPointId].transform.position,
                                spawnPoints[spawnPointId].transform.rotation, enemiesParent)
                            .GetComponent<Enemies>();
        var pointsTransform = spawnPoints[spawnPointId].GetComponent<Waypoints>().Paths[spawnPointId];
        // Set its waypoints, as well as its first waypoint
        enemy.Waypoints = pointsTransform;
        enemy.target = pointsTransform.GetChild(0);
        return enemy.gameObject;
    }

    // Periodically checks whether the spawned wave has been defeated, then updates waveOnGoing
    // variable, and emits a RoundEndEvent.
    public IEnumerator CheckWaveComplete()
    {
        while (true)
        {
            waveOnGoing = false;
            foreach (var enemy in currentWave)
            {
                if (enemy != null)
                {
                    waveOnGoing = true;
                    break;
                }
            }
            if (!waveOnGoing)
            {
                currentWave = null;
                if (OnRoundEnd != null) OnRoundEnd();
                break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}

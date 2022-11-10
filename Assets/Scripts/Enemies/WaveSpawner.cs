using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    // GameObjects
    public Transform[] enemyPrefab;
    GameObject[] spawnPoints;
    public Transform parent;

    public float individualSpawnDelay = 0.5f;

    public List<Transform>[] waves;


    int currentWaveId;
    Transform[] currentWave;
    [HideInInspector]
    public bool waveOnGoing;

    [HideInInspector]
    public delegate void RoundEndEvent();
    [HideInInspector]
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

    void PopulateWave()
    {
        // Wave 1
        for (int wave = 0; wave < 4; wave++)
        {
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
        if (currentWave != null) return;
        switch (currentWaveId)
        {
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
            default:
                StartCoroutine(StartWave(2));
                StartCoroutine(StartWave(3));
                break;

        }
        StartCoroutine(CheckWaveComplete());
        currentWaveId++;
    }

    public IEnumerator StartWave(int spawnPointId)
    {
        int waveId = (currentWaveId > 3) ? 3 : currentWaveId;
        currentWave = new Transform[waves[waveId].Count];
        for (int i = 0; i < waves[waveId].Count; i++)
        {
            // waveCount.text = "Wave: " + (waveGlobalIndex + 1);
            var enemy = SpawnEnemy(waves[waveId][i].gameObject, spawnPointId);
            currentWave[i] = enemy.transform;
            yield return new WaitForSeconds(individualSpawnDelay);
        }
    }
    GameObject SpawnEnemy(GameObject enemyGO, int spawnPointId)
    {
        var enemy = Instantiate(enemyGO,
                                spawnPoints[spawnPointId].transform.position,
                                spawnPoints[spawnPointId].transform.rotation, parent)
                            .GetComponent<Enemies>();
        var pointsTransform = spawnPoints[spawnPointId].GetComponent<Waypoints>().Paths[spawnPointId];
        enemy.Waypoints = pointsTransform;
        enemy.target = pointsTransform.GetChild(0);
        return enemy.gameObject;
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    GameObject[] spawnPoints;
    public Transform parent;

    public float individualSpawnDelay = 0.5f;

    public EnemyWave[] waves;

    public TMPro.TMP_Text waveSpawningCheck;
    public TMPro.TMP_Text waveCount;

    private int waveCurrentIndex;
    private int waveGlobalIndex;

    bool waveOnGoing = false;

    void Start()
    {
        PopulateWave();
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    private void Update()
    {
        // if (waveOnGoing)
        // {
        //     waveSpawningCheck.text = "Wave Spawning";
        // }
        // else
        // {
        //     waveSpawningCheck.text = "Wave Not Spawning";
        // }
    }

    void PopulateWave()
    {
        // Wave 1
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                waves[0].AddEnemy(enemyPrefab[0]);
            }
            else
            {
                waves[0].AddEnemy(enemyPrefab[1]);
            }
        }
    }

    public void ButtonInput()
    {
        if (waveOnGoing) return;
        StartCoroutine(StartWave(2));
        StartCoroutine(StartWave(3));
    }

    public IEnumerator StartWave(int spawnPointId)
    {
        for (int i = 0; i < waves[0].enemyWave.Count; i++)
        {
            waveOnGoing = true;
            // waveCount.text = "Wave: " + (waveGlobalIndex + 1);
            SpawnEnemy(spawnPointId);
            yield return new WaitForSeconds(individualSpawnDelay);
            waveCurrentIndex++;
            if (waveCurrentIndex == waves[waveGlobalIndex].enemyWave.Count)
            {
                waveOnGoing = false;
                waveCurrentIndex = 0;
                waveGlobalIndex++;
            }
        }
    }
    void SpawnEnemy(int spawnPointId)
    {
        var enemyT = Instantiate(waves[waveGlobalIndex].enemyWave[waveCurrentIndex],
                                spawnPoints[spawnPointId].transform.position,
                                spawnPoints[spawnPointId].transform.rotation, parent);
        var enemy = enemyT.GetComponent<Enemies>();
        var pointsTransform = spawnPoints[spawnPointId].GetComponent<Waypoints>().Paths[spawnPointId];
        enemy.Waypoints = pointsTransform;
        enemy.target = pointsTransform.GetChild(0);
    }
}

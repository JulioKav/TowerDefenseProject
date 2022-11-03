using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    public Transform spawnPoint;
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
        // Wave 2
        for (int i = 0; i < 8; i++)
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

    private void Update()
    {
        return;
        if (waveOnGoing)
        {
            waveSpawningCheck.text = "Wave Spawning";
        }
        else
        {
            waveSpawningCheck.text = "Wave Not Spawning";
        }
    }

    public void ButtonInput()
    {
        if (waveOnGoing) return;
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        for (int i = 0; i < waves[0].enemyWave.Count; i++)
        {
            waveOnGoing = true;
            waveCount.text = "Wave: " + (waveGlobalIndex + 1);
            SpawnEnemy();
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
    void SpawnEnemy()
    {
        Instantiate(waves[waveGlobalIndex].enemyWave[waveCurrentIndex], spawnPoint.position, spawnPoint.rotation, parent);
    }
}

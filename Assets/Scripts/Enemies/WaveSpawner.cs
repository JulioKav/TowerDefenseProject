using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform[] enemyPrefab;
    public Transform spawnPoint;
    public Transform parent;

    public float individualSpawnDelay = 0.5f;

    public EnemyWave[] waves;

    private int waveCurrentIndex;
    private int waveGlobalIndex;

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            if(i % 2 == 0)
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
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        for (int i = 0; i < waves[0].enemyWave.Count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(individualSpawnDelay);
            waveCurrentIndex++;
            if(waveCurrentIndex == waves[waveGlobalIndex].enemyWave.Count)
            {
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

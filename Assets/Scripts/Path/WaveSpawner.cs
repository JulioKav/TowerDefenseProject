using System;
using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Transform parent;
    
    public float timeBetweenWaves = 5f;
    public float individualSpawnDelay = 0.5f;
    private float countdown = 0f;

    public int waveMax = 1;
    private int waveIndex = 0;

    public Text waveCountdownText;
    
    void Update()
    {
        //Debug.Log(countdown);
        countdown -= Time.deltaTime;
        if (countdown <= 0 && waveIndex <= waveMax)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        waveCountdownText.text = Mathf.RoundToInt(countdown).ToString();

    }
    //Spawns each wave with delay between individual enemies
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(individualSpawnDelay);
        }
        waveIndex++;
    }
    //Spawns an enemy
    void SpawnEnemy()
    {
         Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation, parent);
    }
}

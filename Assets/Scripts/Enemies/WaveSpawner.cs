using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int WaveNumber = 0;

    public GameObject frogbossPrefab;
    public GameObject batPrefab, broomPrefab, cauldronPrefab, evilbookPrefab, eyeballPrefab, spiderPrefab;
    public Transform[] enemyPrefab;
    // The game object enemies will be attached to
    public Transform enemiesParent;

    public float individualSpawnDelay = 0.25f;

    // Variables for wave management
    List<GameObject> currentWavePrefabs;
    List<GameObject> currentWave;

    WavesJSONParser.Wave[] waves;

    void Start()
    {
        waves = WavesJSONParser.Instance.wavesJson.waves;
        currentWavePrefabs = null;
        WaveNumber = 0;
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
        if (newState == GameState.ROUND_ONGOING) StartRound();
    }

    void StartRound()
    {
        PopulateWave();
        StartCoroutine(SpawnWave());
        StartCoroutine(CheckWaveComplete());
        WaveNumber++;
    }

    void PopulateWave()
    {
        currentWavePrefabs = new List<GameObject>();

        for (int i = 0; i < waves[WaveNumber].bat; i++) currentWavePrefabs.Add(batPrefab);
        for (int i = 0; i < waves[WaveNumber].broom; i++) currentWavePrefabs.Add(broomPrefab);
        for (int i = 0; i < waves[WaveNumber].cauldron; i++) currentWavePrefabs.Add(cauldronPrefab);
        for (int i = 0; i < waves[WaveNumber].evilbook; i++) currentWavePrefabs.Add(evilbookPrefab);
        for (int i = 0; i < waves[WaveNumber].eyeball; i++) currentWavePrefabs.Add(eyeballPrefab);
        for (int i = 0; i < waves[WaveNumber].spider; i++) currentWavePrefabs.Add(spiderPrefab);
        for (int i = 0; i < waves[WaveNumber].frogboss; i++) currentWavePrefabs.Add(frogbossPrefab);
    }

    // Starts spawning a wave in a coroutine with a delay between enemies
    public IEnumerator SpawnWave()
    {
        currentWave = new List<GameObject>();

        List<Transform> spawnPoints = PathGenerator.Instance.activeSpawnPoints;
        while (currentWavePrefabs.Count > 0)
        {
            // Pick a random spawn point from active spawn points
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

            // Choose a random enemy from the current wave to spawn
            int enemyId = Random.Range(0, currentWavePrefabs.Count);
            GameObject enemyPrefab = currentWavePrefabs[enemyId];
            currentWavePrefabs.RemoveAt(enemyId);

            // Spawn the enemy
            Vector3 enemyPos = spawnPoint.position + new Vector3(0, enemyPrefab.transform.position.y, 0);
            var enemy = Instantiate(enemyPrefab, enemyPos, spawnPoint.rotation, enemiesParent);
            currentWave.Add(enemy);

            yield return new WaitForSeconds(individualSpawnDelay);
        }
    }

    // Periodically checks whether the spawned wave has been defeated, then updates waveOnGoing
    // variable, and emits a RoundEndEvent.
    public IEnumerator CheckWaveComplete()
    {
        while (currentWavePrefabs.Count > 0 && currentWave.Count > 0)
        {
            for (int i = currentWave.Count - 1; i >= 0; i--)
                if (currentWave[i] == null) currentWave.RemoveAt(i);

            yield return new WaitForSeconds(1);
        }

        if (WaveNumber == waves.Length - 1) GameStateManager.Instance.EndGame(SkillManager.Instance.finalSkillUnlocked);
        else GameStateManager.Instance.EndRound();
    }
}

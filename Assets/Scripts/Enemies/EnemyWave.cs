using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyWave : MonoBehaviour
{
    public List<Transform> enemyWave;

    public void AddEnemy(Transform enemy)
    {
        enemyWave.Add(enemy);
    }

    public void AddEnemies(Transform[] enemy)
    {
        enemyWave.AddRange(enemy);
    }

    public void DeleteEnemy(Transform enemy)
    {
        enemyWave.Remove(enemy);
    }

    public void DeleteEnemyAtIndex(int index)
    {
        enemyWave.RemoveAt(index);
    }
}

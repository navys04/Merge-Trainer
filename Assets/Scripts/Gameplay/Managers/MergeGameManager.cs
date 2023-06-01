using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeGameManager : SingletonBase<MergeGameManager>
{
    [SerializeField] private int _mergesCountToSpawnEnemy = 20;
    [SerializeField] private GameObject _enemyPrefab;

    private List<Transform> _enemySpawnPoints = new List<Transform>();
    
    private int _mergesCount;

    public void AddMergesCount()
    {
        _mergesCount++;
        CheckCanSpawnEnemy();
    }

    public void SetEnemySpawnPoints(List<Transform> spawnPoints)
    {
        _enemySpawnPoints = spawnPoints;
    }

    public void SetEnemySpawnPoint(Transform[] spawnPoints)
    {
        _enemySpawnPoints.AddRange(spawnPoints);
    }

    private void CheckCanSpawnEnemy()
    {
        if (_mergesCount % _mergesCountToSpawnEnemy == 0)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_enemySpawnPoints.Count == 0) return;

        int rand = Random.Range(0, _enemySpawnPoints.Count);
        Instantiate(_enemyPrefab, _enemySpawnPoints[rand].transform.position,
            _enemySpawnPoints[rand].transform.rotation);
    }
}

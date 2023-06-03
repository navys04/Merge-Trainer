using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MergeGameManager : SingletonBase<MergeGameManager>
{
    [SerializeField] private int _mergesCountToSpawnEnemy = 20;
    [SerializeField] private GameObject _enemyPrefab;
    
    [Header("Missions")]
    [SerializeField] private int _defaultPoints = 10;

    [SerializeField] private int _pointsToNewLevel = 40;

    private int _currentPoints;
    private int _currentPointsToNewLevel;

    private List<Transform> _enemySpawnPoints = new List<Transform>();
    
    private int _mergesCount;
    
    public Action<int> OnMissionUpdated = delegate(int i) {  };
    public Action<int> OnPointsUpdated = delegate(int i) {  };

    private void Start()
    {
        _currentPointsToNewLevel = _defaultPoints;
    }

    public int GetCurrentPoints() => _currentPoints;
    public int GetCurrentPointsToNewLevel() => _currentPointsToNewLevel;
    
    public void StartMission()
    {
        _currentPointsToNewLevel += _pointsToNewLevel;
        _pointsToNewLevel += 10;
        OnMissionUpdated?.Invoke(_currentPointsToNewLevel);
    }

    private void CheckCanStartNewMission()
    {
        if (_currentPoints >= _currentPointsToNewLevel) StartMission();
    }
    
    public void OnUnitSold(int price)
    {
        _currentPoints += price;
        CheckCanStartNewMission();
        OnPointsUpdated?.Invoke(_currentPoints);
    }
    
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

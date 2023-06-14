using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MergeGameManager : SingletonBase<MergeGameManager>
{
    [SerializeField] private int _mergesCountToSpawnEnemy = 20;
    [SerializeField] private GameObject _enemyPrefab;
    
    [Header("Missions")]
    [SerializeField] private int _defaultPoints = 10;
    [SerializeField] private int _pointsToNewLevel = 40;
    private int _currentLevel = 1;
    
    [Header("Submissions")]
    [SerializeField] private int _submissionUnitsCount = 3;
    [SerializeField] private int _submissionUnitLevel = 1;
    [SerializeField] private EUnitType _submissionUnitType;
    [SerializeField] private float _percentRewardForFinishQuest = 0.03f;
    

    private int _currentSubmissionUnitsCount;
    
    private int _currentPoints;
    private int _currentPointsToNewLevel;

    private List<Transform> _enemySpawnPoints = new List<Transform>();
    
    private int _mergesCount;
    
    public Action<int> OnMissionUpdated = delegate(int i) {  };
    public Action<int> OnLevelUpdated = delegate(int i) {  };
    public Action<int> OnPointsUpdated = delegate(int i) {  };
    public Action<int, int, EUnitType> OnSubmissionUpdated = delegate(int i, int j, EUnitType type) {  };

    private void Start()
    {
        _currentPointsToNewLevel = _defaultPoints;
        StartSubmission();
    }
    
    public int GetCurrentPoints() => _currentPoints;
    public int GetCurrentPointsToNewLevel() => _currentPointsToNewLevel;

    public void StartSubmission()
    {
        _submissionUnitsCount = Random.Range(1, 5);
        _submissionUnitLevel = Random.Range(1, 3);
        _submissionUnitType = (EUnitType)Random.Range(0, 2);
        OnSubmissionUpdated?.Invoke(_submissionUnitsCount, _submissionUnitLevel, _submissionUnitType);
    }

    private void CheckCanStartNewSubmission()
    {
        if (_currentSubmissionUnitsCount == _submissionUnitsCount)
        {
            StartSubmission();
            _currentSubmissionUnitsCount = 0;
            _currentPoints += (int)(_currentPoints * _percentRewardForFinishQuest);
            OnPointsUpdated?.Invoke(_currentPoints);
            return;
        }
    }
    
    public void StartMission()
    {
        _currentPointsToNewLevel += _pointsToNewLevel;
        _pointsToNewLevel += 10;
        _currentLevel++;
        OnLevelUpdated?.Invoke(_currentLevel);
        OnMissionUpdated?.Invoke(_currentPointsToNewLevel);
    }

    private void CheckCanStartNewMission()
    {
        if (_currentPoints >= _currentPointsToNewLevel) StartMission();
    }

    public void OnUnitSold(int price, Unit unit)
    {
        _currentPoints += price;
        CheckCanStartNewMission();

        if (unit.GetLevel() == _submissionUnitLevel && unit.GetUnitType() == _submissionUnitType)
        {
            _currentSubmissionUnitsCount++;
            CheckCanStartNewSubmission();
        }

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
        PanelManager panelManager = PanelManager.Instance;
        MergeablePanel mergeablePanel = panelManager.GetFreePanel();
        
        GameObject enemy = Instantiate(_enemyPrefab, mergeablePanel.transform.position,
            mergeablePanel.transform.rotation);

        MergeableObject mergeableObject = enemy.GetComponent<MergeableObject>();
        mergeablePanel.SetObject(mergeableObject);
        /**
        if (_enemySpawnPoints.Count == 0) return;

        int rand = Random.Range(0, _enemySpawnPoints.Count);
        
            */ //DEPRECATED
    }
}

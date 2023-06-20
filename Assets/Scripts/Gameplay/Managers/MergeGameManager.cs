using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        LoadManager();
        
        _currentPointsToNewLevel = _defaultPoints;
        StartSubmission();
    }
    
    public int GetCurrentPoints() => _currentPoints;
    public int GetCurrentPointsToNewLevel() => _currentPointsToNewLevel;
    public int GetCurrentSubmissionUnitsCount() => _currentSubmissionUnitsCount;
    public int GetCurrentLevel() => _currentLevel;

    private void StartSubmission()
    {
        _submissionUnitsCount = Random.Range(1, 5);
        _submissionUnitLevel = Random.Range(1, 2);
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
        
        PlayerSave.Instance.Save();
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
        
        PlayerSave.Instance.Save();
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

    private void LoadManager()
    {
        print(Application.persistentDataPath);
     
        if (PlayerPrefs.HasKey(PlayerSave.CURRENT_POINTS_KEY))
        {
            _currentPoints = PlayerPrefs.GetInt(PlayerSave.CURRENT_POINTS_KEY);
            print("_current points " + _currentPoints);
            OnPointsUpdated?.Invoke(_currentPoints);
        }

        if (PlayerPrefs.HasKey(PlayerSave.CURRENT_POINTS_TO_NEW_LEVEL_KEY))
        {
            _currentPointsToNewLevel = PlayerPrefs.GetInt(PlayerSave.CURRENT_POINTS_TO_NEW_LEVEL_KEY);
            print("_current points " + _currentPointsToNewLevel);
            OnMissionUpdated?.Invoke(_currentPointsToNewLevel);
        }

        if (PlayerPrefs.HasKey(PlayerSave.CURRENT_SUBMISSION_UNITS_COUNT_KEY))
        {
            _currentSubmissionUnitsCount = PlayerPrefs.GetInt(PlayerSave.CURRENT_SUBMISSION_UNITS_COUNT_KEY);
        }
        
        if (PlayerPrefs.HasKey(PlayerSave.LEVEL_KEY))
        {
            _currentLevel = PlayerPrefs.GetInt(PlayerSave.LEVEL_KEY);
            OnLevelUpdated?.Invoke(_currentLevel);
        }
    }

    public void AddResourcesIdle()
    {
        DateTime lastDateTime = PlayerSave.Instance.LoadTime();
        DateTime currentDateTime = DateTime.Now;
        
        if (lastDateTime.Year == 0) return;

        TimeSpan timeSpan = currentDateTime.Subtract(lastDateTime);
        int seconds = (int)timeSpan.TotalSeconds;

        PlayerManager playerManager = PlayerManager.Instance;
        List<MergeableObject> totalObjects = new List<MergeableObject>();
        List<MergeablePanel> panels = PanelManager.Instance.GetPanels();

        foreach (var panel in panels)
        {
            if (!panel.IsPanelFree())
            {
                totalObjects.Add(panel.GetObject());
            }
        }

        TimeManager timeManager = TimeManager.Instance;


        float foodToAdd = timeManager.foodPerSecond * totalObjects.Count * seconds;
        float feedToAdd = timeManager.feedPerSecond * totalObjects.Count * seconds;
        float woodToAdd = timeManager.woodPerSecond * totalObjects.Count * seconds;

        playerManager.AddFood(foodToAdd);
        playerManager.AddFeed(feedToAdd);
        playerManager.AddWood(woodToAdd);
        
        Debug.Log(string.Format("added {0} food, {1} feed, {2} wood", foodToAdd, feedToAdd, woodToAdd));
    }
}

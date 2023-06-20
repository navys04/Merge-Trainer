using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : SingletonBase<PanelManager>
{
    [Header("Load/Save system")] 
    [SerializeField] private List<Unit> _allUnits = new List<Unit>();
    [SerializeField] private Enemy _enemy;

    private readonly List<MergeablePanel> _panels = new List<MergeablePanel>();

    public List<MergeablePanel> GetPanels() => _panels;

    private void Start()
    {
        MergeablePanel[] panels = GetComponentsInChildren<MergeablePanel>();
        _panels.AddRange(panels);
        
        LoadPanels();
        MergeGameManager.Instance.AddResourcesIdle();
    }

    public void AddPanel(MergeablePanel panel) => _panels.Add(panel);

    public MergeablePanel GetFreePanel()
    {
        foreach (var panel in _panels)
        {
            if (panel.IsPanelFree()) return panel;
        }

        return null;
    }

    public List<MergeablePanel> GetFreePanels()
    {
        List<MergeablePanel> mergeablePanels = new List<MergeablePanel>();
        
        foreach (var panel in _panels)
        {
            if (panel.IsPanelFree()) mergeablePanels.Add(panel);
        }

        return mergeablePanels;
    }

    private void LoadPanels()
    {
        for (int i = 0; i < _panels.Count; i++)
        {
            if (PlayerPrefs.HasKey(i.ToString()))
            {
                print("save save");
                string value = PlayerPrefs.GetString(i.ToString());
                string[] parameters = value.Split(",");

                if (parameters[1] != "enemy")
                {
                    EUnitType unitType = EUnitType.Soldier;
                    Enum.TryParse<EUnitType>(parameters[1], out unitType);
                    SpawnUnitAtPanel(i, int.Parse(parameters[0]), unitType);
                }
                
                else SpawnEnemyAtPanel(i);
            }
        }
    }

    private Unit GetUnit(int level, EUnitType unitType)
    {
        foreach (var unit in _allUnits)
        {
            if (unit.GetLevel() == level && unit.GetUnitType() == unitType) return unit;
        }
        
        return null;
    }
    
    private void SpawnUnitAtPanel(int index, int level, EUnitType type)
    {
        Unit unit = GetUnit(level, type);
        if (!unit) return;

        MergeablePanel parentPanelForUnit = _panels[index];

        Unit spawnedUnit = Instantiate(unit, parentPanelForUnit.transform.position,
            parentPanelForUnit.transform.rotation, parentPanelForUnit.transform);
        
        parentPanelForUnit.SetObject(spawnedUnit);
    }

    private void SpawnEnemyAtPanel(int index)
    {
        MergeablePanel parentPanelForEnemy = _panels[index];
        
        Enemy spawnedEnemy = Instantiate(_enemy, parentPanelForEnemy.transform.position,
            parentPanelForEnemy.transform.rotation, parentPanelForEnemy.transform);
        
        parentPanelForEnemy.SetObject(spawnedEnemy);
    }

    public void OnObjectCleared(MergeablePanel panel)
    {
        int index = _panels.IndexOf(panel);
        
        PlayerPrefs.DeleteKey(index.ToString());
    }
}

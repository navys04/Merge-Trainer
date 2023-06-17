using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSave : SingletonBase<PlayerSave>
{
    // Merge game manager keys
    public const string CURRENT_SUBMISSION_UNITS_COUNT_KEY = "currentsubmissionunitscount";
    public const string CURRENT_POINTS_TO_NEW_LEVEL_KEY = "currentpointstonewlevel";
    public const string CURRENT_POINTS_KEY = "currentpointskey";
    public const string LEVEL_KEY = "levelkey";
    
    // Player manager keys
    public const string FOOD_KEY = "food";
    public const string FEED_KEY = "feed";
    public const string WOOD_KEY = "wood";
    public const string GOLD_KEY = "gold";
    public const string DIAMONDS_KEY = "diamonds";

    public void Save()
    {
        // Merge game manager save
        SaveMergeGameManager();
        
        // Player manager save
        SavePlayerManager();
        
        // Panels save
        SavePanels();
        
        PlayerPrefs.Save();
    }

    private void SaveMergeGameManager()
    {
        MergeGameManager mergeGameManager = MergeGameManager.Instance;
        
        PlayerPrefs.SetInt(CURRENT_SUBMISSION_UNITS_COUNT_KEY, mergeGameManager.GetCurrentSubmissionUnitsCount());
        Debug.Log("Saved " + CURRENT_SUBMISSION_UNITS_COUNT_KEY + "by value: " + mergeGameManager.GetCurrentSubmissionUnitsCount());
        
        PlayerPrefs.SetInt(CURRENT_POINTS_TO_NEW_LEVEL_KEY, mergeGameManager.GetCurrentPointsToNewLevel());
        PlayerPrefs.SetInt(CURRENT_POINTS_KEY, mergeGameManager.GetCurrentPoints());
        PlayerPrefs.SetInt(LEVEL_KEY, mergeGameManager.GetCurrentLevel());
    }

    private void SavePlayerManager()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        PlayerPrefs.SetFloat(FOOD_KEY, playerManager.GetFood());
        PlayerPrefs.SetFloat(FEED_KEY, playerManager.GetFeed());
        PlayerPrefs.SetFloat(WOOD_KEY, playerManager.GetWood());
        PlayerPrefs.SetFloat(GOLD_KEY, playerManager.GetGold());
        PlayerPrefs.SetFloat(DIAMONDS_KEY, playerManager.GetDiamonds());
    }

    private void SavePanels()
    {
        PanelManager panelManager = PanelManager.Instance;
        List<MergeablePanel> panels = panelManager.GetPanels();

        for (int i = 0; i < panels.Count; i++)
        {
            MergeablePanel currentPanel = panels[i];
            if (currentPanel.GetObject() == null) continue;

            string value = currentPanel.GetObject().GetLevel().ToString() + ",";
            if (currentPanel.GetObject().TryGetComponent(out Unit unit))
            {
                value += unit.GetUnitType().ToString();
            }

            else value += "enemy";
            
            PlayerPrefs.SetString(i.ToString(), value);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

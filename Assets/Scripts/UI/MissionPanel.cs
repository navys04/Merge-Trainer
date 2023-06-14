using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private Text _missionText;
    [SerializeField] private Text _levelText;
    [SerializeField] private Image _fillImage;
    
    private void Start()
    {
        MergeGameManager mergeGameManager = MergeGameManager.Instance;
        
        mergeGameManager.OnMissionUpdated += OnMissionUpdated;
        mergeGameManager.OnPointsUpdated += OnPointsUpdated;
        mergeGameManager.OnLevelUpdated += OnLevelUpdated;
    }

    private void OnLevelUpdated(int value)
    {
        _levelText.text = value.ToString();
    }
    
    private void OnPointsUpdated(int curPoints)
    {
        int curPointsToNewLevel = MergeGameManager.Instance.GetCurrentPointsToNewLevel();
        
        _missionText.text = curPoints + " / " + curPointsToNewLevel;

        _fillImage.fillAmount = (float)curPoints / curPointsToNewLevel;
    }
    
    private void OnMissionUpdated(int points)
    {
        int curPoints = MergeGameManager.Instance.GetCurrentPoints();

        _missionText.text = curPoints + " / " + points;
        _fillImage.fillAmount = 0;
    }
}

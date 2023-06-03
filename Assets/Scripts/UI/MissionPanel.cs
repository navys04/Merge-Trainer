using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _missionText;
    
    private void Start()
    {
        MergeGameManager.Instance.OnMissionUpdated += OnMissionUpdated;
        MergeGameManager.Instance.OnPointsUpdated += OnPointsUpdated;
    }

    private void OnPointsUpdated(int curPoints)
    {
        int curPointsToNewLevel = MergeGameManager.Instance.GetCurrentPointsToNewLevel();
        
        _missionText.text = curPoints + " / " + curPointsToNewLevel;
    }
    
    private void OnMissionUpdated(int points)
    {
        int curPoints = MergeGameManager.Instance.GetCurrentPoints();

        _missionText.text = curPoints + " / " + points;
    }
}

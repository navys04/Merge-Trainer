using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [SerializeField] private Text _missionText;
    [SerializeField] private Image _fillImage;
    
    private void Start()
    {
        MergeGameManager.Instance.OnMissionUpdated += OnMissionUpdated;
        MergeGameManager.Instance.OnPointsUpdated += OnPointsUpdated;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubquestPanel : MonoBehaviour
{
    [SerializeField] private Text _unitText;
    [SerializeField] private Text _unitCountText;
    [SerializeField] private Image _unitImage;

    private void Start()
    {
        MergeGameManager.Instance.OnSubmissionUpdated += OnSubmissionUpdated;
    }

    private void OnSubmissionUpdated(int submissionCount, int submissionLevel, EUnitType type)
    {
        _unitText.text = type.ToString() + " lvl " + submissionLevel;
        _unitCountText.text = "x" + submissionCount;
    }
}

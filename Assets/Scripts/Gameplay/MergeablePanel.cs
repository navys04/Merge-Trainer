using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeablePanel : MonoBehaviour
{
    private MergeableObject _mergeableObjectInPanel;

    private void Start()
    {
        MergeableObject mergeableObject = GetComponentInChildren<MergeableObject>();
        if (mergeableObject) SetObject(mergeableObject);
    }

    public MergeableObject GetObject() => _mergeableObjectInPanel;

    public void SetObject(MergeableObject newMergeableObject)
    {
        _mergeableObjectInPanel = newMergeableObject;
        _mergeableObjectInPanel.SetParentPanel(this);
    }
    
    public void ClearObject() => _mergeableObjectInPanel = null;
    
    public bool IsPanelFree() => _mergeableObjectInPanel == null;
}

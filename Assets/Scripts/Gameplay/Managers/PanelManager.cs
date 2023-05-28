using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : SingletonBase<PanelManager>
{
    private List<MergeablePanel> _panels = new List<MergeablePanel>();

    private void Start()
    {
        MergeablePanel[] panels = GetComponentsInChildren<MergeablePanel>();
        _panels.AddRange(panels);
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
}

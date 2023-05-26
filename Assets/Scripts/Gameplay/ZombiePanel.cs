using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePanel : MonoBehaviour
{
    private Zombie _zombieInPanel;

    private void Start()
    {
        Zombie zombie = GetComponentInChildren<Zombie>();
        if (zombie) SetZombie(zombie);
    }

    public Zombie GetZombie() => _zombieInPanel;

    public void SetZombie(Zombie newZombie)
    {
        _zombieInPanel = newZombie;
        _zombieInPanel.SetParentPanel(this);
    }
    
    public void ClearZombie() => _zombieInPanel = null;
    
    public bool IsPanelFree() => _zombieInPanel == null;
}

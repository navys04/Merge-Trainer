using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MergeableObject, IDamageable
{
    public float Health { get; set; }

    [SerializeField] private float _diamondsToAdd = 14;

    private void Start()
    {
        _isActiveForMerge = false;
        Health = 100.0f;
    }

    public void Damage(float value)
    {
        Health -= value;
        if (Health <= 0.0f)
        {
            _parentPanel.ClearObject();
            PlayerManager.Instance.AddDiamonds(_diamondsToAdd);
            Destroy(gameObject);
        }
    }
}

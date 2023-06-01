using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float Health { get; set; }

    private void Start()
    {
        Health = 100.0f;
    }

    public void Damage(float value)
    {
        Health -= value;
        if (Health <= 0.0f) Destroy(gameObject);
    }
}

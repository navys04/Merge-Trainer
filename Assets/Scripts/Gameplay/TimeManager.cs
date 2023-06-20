using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : SingletonBase<TimeManager>
{
    [Header("When player is not in the game")]
    public float foodPerSecond;
    public float feedPerSecond;
    public float woodPerSecond;
    
    private DateTime _currentDateTime;
    public DateTime GetCurrentDateTime() => _currentDateTime;
    
    private void OnApplicationQuit()
    {
        _currentDateTime = DateTime.Now;
    }
}

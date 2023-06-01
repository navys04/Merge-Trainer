using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    void Start()
    {
        MergeGameManager mergeGameManager = MergeGameManager.Instance;

        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }
        
        mergeGameManager.SetEnemySpawnPoints(children);
    }
}

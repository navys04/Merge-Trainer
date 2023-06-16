using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseSetup : MonoBehaviour
{
    private Firebase.FirebaseApp _app;
    
    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                _app = Firebase.FirebaseApp.DefaultInstance;
            }

            else
            {
                Debug.LogError(System.String.Format("Could not resolve all firebase dependencies: {0}",
                    dependencyStatus));
            }
        });
    }
}

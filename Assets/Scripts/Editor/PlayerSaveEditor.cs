using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerSave))]
public class PlayerSaveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Delete saves"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}

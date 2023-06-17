using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeablePanel : MonoBehaviour
{
    private MergeableObject _mergeableObjectInPanel;
    
    [Header("FX")] 
    [SerializeField] private ParticleSystem _mergeReadyFX;

    [SerializeField] private ParticleSystem _onMergeFX;

    private ParticleSystem _mergeFXSpawned;
    private ParticleSystem _onMergeFXSpawned;
    private void Start()
    {
        MergeableObject mergeableObject = GetComponentInChildren<MergeableObject>();
        if (mergeableObject) SetObject(mergeableObject);
        
        SpawnMergeFX();
    }

    private void SpawnMergeFX()
    {
        if (!_mergeReadyFX) return;

        _mergeFXSpawned = Instantiate(_mergeReadyFX, transform.position, transform.rotation, transform);
        _onMergeFXSpawned = Instantiate(_onMergeFX, transform.position, transform.rotation, transform);
    }

    public MergeableObject GetObject() => _mergeableObjectInPanel;

    public void SetObject(MergeableObject newMergeableObject)
    {
        _mergeableObjectInPanel = newMergeableObject;
        _mergeableObjectInPanel.SetParentPanel(this);

        _onMergeFXSpawned.Play();
    }

    public void ClearObject()
    {
        _mergeableObjectInPanel = null;
        
        PanelManager.Instance.OnObjectCleared(this);
    }
    
    public bool IsPanelFree() => _mergeableObjectInPanel == null;
    
    public void ChangeMergeReadyFXState(bool isPlaying)
    {
        if (isPlaying && !_mergeFXSpawned.isPlaying) _mergeFXSpawned.Play();
        else _mergeFXSpawned.Stop();
       // _mergeFXSpawned.loop = true;
        
        print("playing fx");
    }
}

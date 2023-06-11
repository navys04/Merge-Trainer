using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public abstract class DragAndDropable : MonoBehaviour
{
    [SerializeField] private float _upTransform = 5.0f;
    [SerializeField] protected bool _isActiveForMerge = true;
    
    private bool _isDragging;

    private Camera _mainCamera;

    private float _yTransformInternal;

    protected void Start()
    {
        _mainCamera = Camera.main;
        _yTransformInternal = transform.position.y + _upTransform;
    }

    private void Update()
    {
        if (!_isActiveForMerge) return;
        
        if (_isDragging) Drag();

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _isDragging = false;
            Drop();
        }
    }

    private void OnMouseDrag()
    {
        _isDragging = true;
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        Drop();
    }

    protected virtual void Drag()
    {
        Ray castPoint = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(hit.point.x, _yTransformInternal, hit.point.z);
        }
    }

    protected virtual void Drop()
    {
        
    }
}

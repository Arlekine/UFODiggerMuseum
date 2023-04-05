using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StadMove : MonoBehaviour
{
    public Stand Stand;
    public StadAreaZone StandArea;
    public Color AvailableBuild;
    public Color NotAvailableBuild;
    public Image BuildZone;
    public GameObject Canvas;
    private float _mouseZ;
    private Vector3 _offset;
    private Transform _standTransform;

    private Vector3 _startStandDragPosition;
    private void Start()
    {
        _standTransform = Stand.transform;
        BuildZone.color = AvailableBuild;
    }

    private void OnEnable()
    {
        Canvas.SetActive(true);
    }
    private void OnDisable()
    {
        Canvas.SetActive(false);
    }
    private void OnMouseDown()
    {
        _mouseZ = Camera.main.WorldToScreenPoint(_standTransform.position).z;
        var newPosition = GetMouseWorldPosition();
        _startStandDragPosition = _standTransform.position;
        
        _offset = _standTransform.position -  new Vector3(newPosition.x,_standTransform.position.y,newPosition.z);

        if (StandArea.IsAreaFree)
        {
            BuildZone.color = AvailableBuild;
        }
        else
        {
            BuildZone.color = NotAvailableBuild;
        }
    }

    private void OnMouseDrag()
    {
        var newPosition = GetMouseWorldPosition(); 
        _standTransform.position = _offset+ new Vector3(newPosition.x,_standTransform.position.y,newPosition.z);
        if (StandArea.IsAreaFree)
        {
            BuildZone.color = AvailableBuild;
        }
        else
        {
            BuildZone.color = NotAvailableBuild;
        }
    }
    private void OnMouseUp()
    {
        if (StandArea.IsAreaFree)
        {
            BuildZone.color = AvailableBuild;
            Stand.SaveStandData();
            NavMeshController.Instance.RebuildNavMesh();
        }
        else
        {
            BuildZone.color = AvailableBuild;
            _standTransform.position = _startStandDragPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZ;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    
}

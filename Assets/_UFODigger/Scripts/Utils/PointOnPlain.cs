using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PointOnPlain : MonoBehaviour
{
    private Transform _plane;

    private Vector3 _leftTop;
    private Vector3 _rightTop;
    private Vector3 _leftBottom;
    private Vector3 _rightBottom;

    private Vector3 _xAxis;
    private Vector3 _zAxis;


    private void Start()
    {
        _plane = GetComponent<Transform>();

        List<Vector3> VerticeList = new List<Vector3>(_plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        _leftTop = _plane.TransformPoint(VerticeList[0]);
        _rightTop = _plane.TransformPoint(VerticeList[10]);
        _leftBottom = _plane.TransformPoint(VerticeList[110]);
        _rightBottom = _plane.TransformPoint(VerticeList[120]);
        _xAxis = _rightTop - _leftTop;
        _zAxis = _leftBottom - _leftTop;
       
    }

    public Vector3 GetRandomPoint()
    {
        var RndPointonPlane = _leftTop + _xAxis * Random.value + _zAxis * Random.value;
        return RndPointonPlane;
    }
}

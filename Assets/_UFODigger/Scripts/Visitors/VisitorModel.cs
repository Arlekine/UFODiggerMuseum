using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisitorModel : MonoBehaviour
{
    public UnityEvent<GameObject> OnModelInstantiate;
    [SerializeField] private Transform _visitorPosition;

    [SerializeField] private GameObject[] _visitorModels;

    private GameObject _model;
    private void Start()
    {
        var randomVisitorModel = _visitorModels[Random.Range(0, _visitorModels.Length)];
        var visiotor = Instantiate(randomVisitorModel, _visitorPosition.position, Quaternion.identity, transform);

        OnModelInstantiate.Invoke(visiotor);

        _model = visiotor;
    }

}

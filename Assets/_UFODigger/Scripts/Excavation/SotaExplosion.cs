using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SotaExplosion : MonoBehaviour
{
    [SerializeField] private int _destroyForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Transform _pointOfexplosion;
    [SerializeField] private float _destroyPartDelay;

    private Rigidbody[] _pieces;

    private void Awake()
    {
        _pieces = GetComponentsInChildren<Rigidbody>();
    }


    public void Destroy()
    {
       
        foreach (var piece in _pieces)
        {
            piece.isKinematic = false;
            piece.AddExplosionForce(_destroyForce, _pointOfexplosion.position, _explosionRadius);
            piece.velocity *= 0.0003f;
        }

        StartCoroutine(DestroyParts());
    }

    IEnumerator DestroyParts()
    {
        yield return new WaitForSeconds(_destroyPartDelay);
        Destroy(gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;

    private float _originalPositionY;

    private float _elapsed;
    private const float _timeWaitForLayerSetup = 0.2f;

    private bool _setupSuccess;

    private void Start()
    {
        StartCoroutine(WaitForLayerSetup());
    }

    IEnumerator WaitForLayerSetup()
    {
        yield return new WaitForSeconds(_timeWaitForLayerSetup);
        _setupSuccess = true;
        _originalPositionY = transform.position.y;
    }

    public void StartShake()
    {
        if (_setupSuccess)
            StartCoroutine(ShakeSota());
    }

    private IEnumerator ShakeSota()
    {
        _elapsed = 0f;

        while (_elapsed < _duration)
        {
            var y = Random.Range(-1, 1) * _magnitude;

            transform.position = new Vector3(transform.position.x, _originalPositionY + y, transform.position.z);
            _elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = new Vector3(transform.position.x, _originalPositionY, transform.position.z);
    }
}

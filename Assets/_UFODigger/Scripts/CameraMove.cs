using System.Collections;
using UnityEngine;
using Utils;

public class CameraMove : Singleton<CameraMove>
{
    [SerializeField] private float _lerpParameter = 2.0f;
    [SerializeField] private float _moveSensivity = 2.0f;
    [SerializeField] private float _timeForMove = 0.5f;
    [SerializeField] private Camera _camera;

    [SerializeField] private Transform _rightUpExtremePoint;
    [SerializeField] private Transform _leftDownExtremePoint;

    public bool NeedSave;
    private Vector3 _mouseDragPosition = Vector3.zero;
    private Vector3 _currentPosition = Vector3.zero;
    private Vector3 _cameraPosition = Vector3.zero;
    private Vector3 _targetPosition;

    private bool _dragging = false;

    private bool _cameraMouseControlActive;

    private bool _blockNewTap;

    private const string _posXKey = "CamX";
    private const string _posZKey = "CamZ";

    private void Start()
    {
        if (NeedSave)
        {
            Load();
        }

        _cameraPosition = _camera.transform.position;
        _cameraMouseControlActive = true;
    }

    public void TurnOffMouseCameraControl()
    {
        _targetPosition = Camera.main.transform.position;
        _cameraMouseControlActive = false;
    }

    public void TurnOnMouseCameraControl()
    {
        _dragging = false;
        _cameraMouseControlActive = true;
    }

    public void MoveCamera(Vector3 targetPosition, float zOffset = 0f)
    {
        if (_cameraMouseControlActive)
            return;

        var targetCameraPosition = new Vector3(targetPosition.x,
            _camera.transform.position.y,
            targetPosition.z - zOffset);

        StartCoroutine(MovingCamera(targetCameraPosition));
    }

    IEnumerator MovingCamera(Vector3 target)
    {
        var elapsedTime = 0f;
        var startPosition = _camera.transform.position;

        while (elapsedTime < _timeForMove)
        {
            _camera.transform.position = Vector3.Lerp(startPosition, target, elapsedTime / _timeForMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        if (!_cameraMouseControlActive)
            return;

        if (Input.GetMouseButtonDown(0) && !_blockNewTap)
        {
            _mouseDragPosition = Input.mousePosition;
            _cameraPosition = _camera.transform.position;
            _blockNewTap = true;
        }

        if (Input.GetMouseButton(0))
        {
            _currentPosition = Input.mousePosition;
            LeftMouseDrag();
            _dragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _blockNewTap = false;
        }

        if (_dragging)
        {
            _camera.transform.position =
                Vector3.Lerp(_camera.transform.position, _targetPosition, Time.deltaTime * _lerpParameter);

            CheckCameraPosition();

            if (Camera.main.transform.position == _targetPosition)
                _dragging = false;
        }
    }

    private void CheckCameraPosition()
    {
        //camera Y fix
        _camera.transform.position = new Vector3(_camera.transform.position.x,
            _cameraPosition.y,
            _camera.transform.position.z);


        if (_camera.transform.position.x > _rightUpExtremePoint.position.x)
        {
            _camera.transform.position = new Vector3(_rightUpExtremePoint.position.x,
                _camera.transform.position.y,
                _camera.transform.position.z);
        }

        if (_camera.transform.position.x < _leftDownExtremePoint.position.x)
        {
            _camera.transform.position = new Vector3(_leftDownExtremePoint.position.x,
                _camera.transform.position.y,
                _camera.transform.position.z);
        }

        if (_camera.transform.position.z > _rightUpExtremePoint.position.z)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                _camera.transform.position.y,
                _rightUpExtremePoint.position.z);
        }

        if (_camera.transform.position.z < _leftDownExtremePoint.position.z)
        {
            _camera.transform.position = new Vector3(_camera.transform.position.x,
                _camera.transform.position.y,
                _leftDownExtremePoint.position.z);
        }
    }

    private void LeftMouseDrag()
    {
        _currentPosition.z = _mouseDragPosition.z = _cameraPosition.y;

        Vector3 direction = _camera.ScreenToWorldPoint(_currentPosition) -
                            _camera.ScreenToWorldPoint(_mouseDragPosition);
        //inversion of camera controll
        direction = direction * -1;

        _targetPosition = _cameraPosition + direction * _moveSensivity;
    }

    public void Save()
    {
        SaveLoadSystem.Save(_posXKey, transform.position.x);
        SaveLoadSystem.Save(_posZKey, transform.position.z);
    }

    private void OnApplicationQuit()
    {
        if (NeedSave)
        {
            Save();
        }
       
    }

    private void OnDestroy()
    {
        if (NeedSave)
        {
            Save();
        }
    }

    private void Load()
    {
        //Load Position
        if (SaveLoadSystem.CheckKey(_posXKey) && SaveLoadSystem.CheckKey(_posZKey))
        {
            transform.position = new Vector3(SaveLoadSystem.LoadFloat(_posXKey),
                transform.position.y,
                SaveLoadSystem.LoadFloat(_posZKey));
        }
    }
}
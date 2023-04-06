using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;
using Utils;
using UnityEngine.EventSystems;
public class Excavation : Singleton<Excavation>
{
    public PLayerData PlayerData;
    [SerializeField] private PlayerInstrument _playerInstrument;
    [SerializeField] private LayerMask _ignoreMe;
    [SerializeField] private PlayerExcavationTurns _playerExcavationTurns;

    private List<Vector3> _rayStartPosition = new List<Vector3>();

    private bool _showRay;

    private float _rayYOffset = 20f;

    private List<ExcavationRay> _excavationRays = new List<ExcavationRay>();

    private PointerEventData _pointerEventData = new PointerEventData(EventSystem.current);
    private List<RaycastResult> _touchResults = new List<RaycastResult>();
    public void BuildRay(Vector3 centr)
    {
        if (!_playerExcavationTurns.CanExcavate || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (IsAboveUI(touch.position))
                {
                    return;
                }
            }
        }

        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began ){
            if(EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return;
        }
        _playerExcavationTurns.Excavate();

        var upCentr = new Vector3(centr.x, centr.y + _rayYOffset, centr.z);

        var instrument = _playerInstrument.GetInstrument();

        //return old instrument after use dinamite
        if (_playerInstrument.IsOneTurnInstrument)
        {
            _playerInstrument.ReturnOldInstrument();
        }

        var instrumentPower = instrument.GetPartPower();
        var raysPositiobDiff = instrument.GetDiff();

        //Set first ray
        _excavationRays.Add(new ExcavationRay(upCentr, instrumentPower[0]));

        for (int i = 0; i < raysPositiobDiff.Count; i++)
        {
            var rayPosition = upCentr - raysPositiobDiff[i];
            rayPosition = new Vector3(rayPosition.x, rayPosition.y + _rayYOffset, rayPosition.z);

            _excavationRays.Add(new ExcavationRay(rayPosition, instrumentPower[i + 1]));
        }


        Dig();

    }

    [EditorButton]
    public void SetDinamite()
    {
        _playerInstrument.SetInstrumentOnOneTurn();
    }

    private bool IsAboveUI(Vector2 touchPos)
    {
        _pointerEventData.position = touchPos;
        EventSystem.current.RaycastAll(_pointerEventData, _touchResults);
        return _touchResults.Count > 0;
    }

    private void Dig()
    {
        if (!PlayerData.IsTutorialComplete)
        {
            FindObjectOfType<TutorialManager>().NextTutorialTurn();
            PlayerData.IsTutorialComplete = true;
            PlayerData.SaveData();

            //AppMetricaManager.instance.TutorialFinish();
            //AppMetricaManager.instance.TutorialState("Finish Tutorial");
        }

        if (UISettingsVibro._vibroStatus)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        for (int i = 0; i < _excavationRays.Count; i++)
        {
            DettectSota(i);
        }

        _excavationRays = new List<ExcavationRay>();
    }

    private void DettectSota(int i)
    {
        RaycastHit hit;
        if (Physics.Raycast(_excavationRays[i].Position, Vector3.down, out hit, Mathf.Infinity, ~_ignoreMe))
        {
            if (hit.transform.TryGetComponent<Sota>(out Sota sota))
            {
                var sotaHealth = sota.Health;
                if (_excavationRays[i].Power > 0)
                {
                    sota.RemoveHealth(_excavationRays[i].Power);
                }
                else
                {
                    sota.Shake();
                }
                _excavationRays[i].Power -= sotaHealth;

                if (_excavationRays[i].Power > 0)
                {
                    DettectSota(i);
                }
            }
        }
    }

    private void Update()
    {
        if (_showRay)
        {
            foreach (var ray in _excavationRays)
            {
                Debug.DrawRay(ray.Position, Vector3.down * 10000000, Color.red);
            }

        }
    }
}

public class ExcavationRay
{
    public int Power;
    public Vector3 Position;

    public ExcavationRay(Vector3 position, int power)
    {
        Power = power;
        Position = position;
    }

}

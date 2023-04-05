using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Sota : MonoBehaviour
{
    [HideInInspector] public int Health = 5;

    public UnityEvent<Sota> OnSotaDestroy;

    public Coin Coin;
    [Range(0f, 1f)] public float CoinChance;

    private bool _isCoinIn;
    public bool IsSotaDestroyed { get; private set; }
    public bool IsFree;

    [SerializeField] private Mesh _damagedSota;
    [SerializeField] private Mesh _preDiedSota;

    [SerializeField] private GameObject _poofVFX;
    [SerializeField] private GameObject _hitVFX;

    [SerializeField] private Transform _hitPoint;

    [SerializeField] private GameObject _partialSota;

    [SerializeField] private MeshRenderer _sotaMesh;
    [SerializeField] private MeshCollider _collider;

    [SerializeField] private GameObject _wallSota;

    [SerializeField] private GameObject _iceFakeSota;


    [SerializeField] private Material _detectMaterial;

    [SerializeField] private Transform _sotaTransform;

    private int _totalHealth;

    [SerializeField] private MeshFilter _meshFilter;

    [SerializeField] private float _maxRandomRotation;

    private Shake _shake;

    private bool _isSotaFake;

    private void Awake()
    {
        var coinChance = Random.Range(0f, 1f);
        if (coinChance > CoinChance)
        {
            _isCoinIn = false;
            Coin.gameObject.SetActive(false);
        }
        else
        {
            _isCoinIn = true;
            Coin.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        _totalHealth = Health;
        _shake = GetComponent<Shake>();

        _sotaTransform.localEulerAngles = new Vector3(
            _sotaTransform.localEulerAngles.x + UnityEngine.Random.Range(-_maxRandomRotation, _maxRandomRotation),
            _sotaTransform.localEulerAngles.y + UnityEngine.Random.Range(-_maxRandomRotation, _maxRandomRotation),
            _sotaTransform.localEulerAngles.z + UnityEngine.Random.Range(-_maxRandomRotation, _maxRandomRotation));
    }

    public void SetSotaDetect()
    {
        _sotaMesh.material = _detectMaterial;
    }

    public void SetSotaWall()
    {
        _wallSota.SetActive(true);
        _sotaMesh.enabled = false;
        _collider.enabled = false;

        _isCoinIn = false;
        Coin.gameObject.SetActive(false);
    }

    public void SetFakeIceSota()
    {
        _iceFakeSota.SetActive(true);
        _sotaMesh.enabled = false;
        _wallSota.SetActive(false);
        _isSotaFake = true;

        _isCoinIn = false;
        Coin.gameObject.SetActive(false);
    }

    public void RemoveSota()
    {
        _wallSota.SetActive(false);
        _iceFakeSota.SetActive(false);
        _sotaMesh.enabled = false;
        _collider.enabled = false;

        _isCoinIn = false;
        Coin.gameObject.SetActive(false);
    }

    public void Shake()
    {
        _shake.StartShake();
    }

    public void RemoveHealth(int attackPower)
    {
        Health -= attackPower;

        Shake();

        if (!_isSotaFake)
            HitVFX();

        if (Health < 1)
        {
            if (Coin.isActiveAndEnabled)
                Coin.CollectWorld();

            if (_isSotaFake)
                _iceFakeSota.SetActive(false);

            gameObject.layer = 2;

            var newDestroyFX = Instantiate(_poofVFX, transform.position, Quaternion.identity, transform.parent.parent);
            newDestroyFX.GetComponent<ParticleSystem>().Play();

            _sotaMesh.enabled = false;
            _collider.enabled = false;

            IsSotaDestroyed = true;

            OnSotaDestroy.Invoke(this);
        }
        else
        {
            if (!_isSotaFake)
                ChangeSotaMesh();
        }
    }

    private void HitVFX()
    {
        var newHitFX = Instantiate(_hitVFX, _hitPoint.position, Quaternion.Euler(-90, 0, 0), transform.parent.parent);
        newHitFX.GetComponent<ParticleSystem>().Play();
    }

    private void ChangeSotaMesh()
    {
        if (Health > _totalHealth * 0.35f && Health <= _totalHealth * 0.65f)
        {
            transform.localEulerAngles = new Vector3(0, 60 * UnityEngine.Random.Range(0, 6), 0);
            _meshFilter.sharedMesh = _damagedSota;
        }

        if (Health <= _totalHealth * 0.35f)
        {
            _meshFilter.sharedMesh = _preDiedSota;
        }
    }

    public void SetFreeSota()
    {
        _wallSota.SetActive(false);
    }

    private void OnMouseDown()
    {
        Excavation.Instance.BuildRay(transform.position);
    }
}
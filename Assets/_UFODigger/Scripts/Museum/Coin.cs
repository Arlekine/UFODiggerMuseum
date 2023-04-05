using System;
using System.Collections;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool IsMuseumCoin;
    
    public int StartCoinGoldCount = 5;
    public UpgradesSO Income;

    public float DelayBetweenWorldCollect;
    public GameObject CoinVfx;
    public float UpHeight;
    public float TimeToUp;
    public float TimeToCollect = 1f;
    public float CollectRadius = 5f;
    public float ZOffset;
    public float YOffset;
    private Vector3 _startPosition;
    private GameObject _vfx;

    private bool _isMoveFinished = true;

    private CoinsCollector _coinsCollector;
    private void Start()
    {
        if (IsMuseumCoin)
        {
            _coinsCollector = FindObjectOfType<CoinsCollector>();
            if (_coinsCollector != null)
            {
                _coinsCollector.AddCoin(transform.position);
            }
        }
    }

    public void MoveUp()
    {
        _isMoveFinished = false;
        _startPosition = transform.position;
        var targetPosition = new Vector3(_startPosition.x,
            UpHeight,
            _startPosition.z);
        StartCoroutine(Move(_startPosition, targetPosition, false));
    }

    IEnumerator Move(Vector3 from, Vector3 to, bool endMove)
    {
        var elapsedTime = 0f;
        while (elapsedTime < TimeToUp)
        {
            transform.position = Vector3.Lerp(from, to, elapsedTime / TimeToUp);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if (!endMove)
            StartCoroutine(Move(to, from, true));
        else
        {
            _vfx = Instantiate(CoinVfx, transform.position, Quaternion.identity);
            _isMoveFinished = true;
        }
    }

    private void OnMouseDown()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, CollectRadius);
        if (UISettingsVibro._vibroStatus)
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
        }

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Coin>(out Coin coin))
            {
                coin.Collect();
            }
        }
    }

    public void Collect()
    {
        if (!_isMoveFinished)
            return;
        
        if (IsMuseumCoin)
        {
            _coinsCollector = FindObjectOfType<CoinsCollector>();
            if (_coinsCollector != null)
            {
                _coinsCollector.Remove(transform.position);
            }
        }
        
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(MoveToCollectZone());
        if (_vfx != null)
            Destroy(_vfx);
    }

    IEnumerator MoveToCollectZone()
    {
        var elapsedTime = 0f;
        var startPosition = transform.position;
        while (elapsedTime < TimeToCollect)
        {
            var goldCollectZone = PlayerManager.Instance._goldCanvasCollectZone.position;

            var targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(goldCollectZone.x,
                goldCollectZone.y + YOffset,
                goldCollectZone.z + ZOffset));

            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / TimeToCollect);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        PlayerManager.Instance._playerData.AddGold(GoldCount());
        Destroy(gameObject);
    }

    public int GoldCount()
    {
        return  StartCoinGoldCount + (int) (StartCoinGoldCount * ((float) Income.GetTotalPower() / 100));
    }

    public void CollectWorld()
    {
        if (!_isMoveFinished)
            return;
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(MoveToWorldCollectZone());
        if (_vfx != null)
            Destroy(_vfx);
    }

    IEnumerator MoveToWorldCollectZone()
    {
        var elapsedUpTime = 0f;
        var startPosition = transform.position;
        while (elapsedUpTime < TimeToUp)
        {
            var targetPosition = new Vector3(transform.position.x,
                transform.position.y + UpHeight,
                transform.position.z);

            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedUpTime / TimeToUp);
            elapsedUpTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(DelayBetweenWorldCollect);
        
        var elapsedTime = 0f;
        startPosition = transform.position;
        while (elapsedTime < TimeToCollect)
        {
            var goldCollectZone = PlayerManager.Instance._worldCollectZone.position;

            transform.position = Vector3.Lerp(startPosition, goldCollectZone, elapsedTime / TimeToCollect);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        var goldCount = StartCoinGoldCount + (int) (StartCoinGoldCount * ((float) Income.GetTotalPower() / 100));
        PlayerManager.Instance._playerData.AddGold(goldCount);
        Destroy(gameObject);
    }
}
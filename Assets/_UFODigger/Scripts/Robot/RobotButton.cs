using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotButton : MonoBehaviour
{
    [SerializeField] private PLayerData _pLayerData;
    [SerializeField] private ADSManager _adsManager;
    [SerializeField] private GameObject _giftWindow;
    [SerializeField] private GameObject _buyParts;
    [SerializeField] private GameObject _activeParts;
    [SerializeField] private TextMeshProUGUI _expireTime;
    [SerializeField] private RobotCleaner _robotCleaner;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Animation _buttonAnimation;
    [SerializeField] private CameraMove cameraMove;

    private void Start()
    {
        gameObject.SetActive(false);
        if (_pLayerData.IsTutorialComplete)
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            StartCoroutine(WaitForGiftsDelay());
        }
    }

    private void Update()
    {
        if (_pLayerData.IsRobotActive)
        {
            TimeSpan expireSpan = (_pLayerData.RobotExpireTime - DateTime.Now);
            _expireTime.text = String.Format("{0:00} : {1:00}", expireSpan.Minutes, expireSpan.Seconds);
        }
        else
        {
            _buttonAnimation.Play();
            _buyParts.SetActive(true);
            _activeParts.SetActive(false);
        }
    }

    private IEnumerator WaitForGiftsDelay()
    {
        yield return new WaitForSeconds(1.5f);

        _canvasGroup.alpha = 1f;
        if (_pLayerData.IsRobotActive)
        {
            _buyParts.SetActive(false);
            _activeParts.SetActive(true);

            _robotCleaner.StartRobot();
        }
        else
        {
            _buyParts.SetActive(true);
            _activeParts.SetActive(false);
        }
    }

    public void ButtonClick()
    {
        if (_pLayerData.IsRobotActive == false)
        {
            cameraMove.TurnOffMouseCameraControl();
            _giftWindow.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void DeclineGift()
    {
        cameraMove.TurnOnMouseCameraControl();
        _giftWindow.SetActive(false);
        gameObject.SetActive(true);
    }

    public void AcceptGift()
    {
        cameraMove.TurnOnMouseCameraControl();
        _adsManager.ShowRewardedAds(ADSManager.RewordTypes.Robot);
        _giftWindow.SetActive(false);
    }

    public void ActivateRobot()
    {
        _buttonAnimation.Stop();
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        
        gameObject.SetActive(true);
        _buyParts.SetActive(false);
        _activeParts.SetActive(true);

        _pLayerData.RobotExpireTime = DateTime.Now.AddMinutes(_robotCleaner.MinutesOfWorking);

        _robotCleaner.StartRobot();
    }
}
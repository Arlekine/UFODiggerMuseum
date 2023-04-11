using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GiftController : MonoBehaviour
{
    public PLayerData PlayerData;
    public int VisitorCount;

    public Stand[] Stands;
    
    public Bus _giftBus;

    [Header("Delay in seconds betwen gifts")]
    [SerializeField] private float _delayBetwenGifts;

    [SerializeField] private TextMeshProUGUI _visitorCountText;
    [SerializeField] private TextMeshProUGUI _visitorCounter;
    [SerializeField] private CameraMove cameraMove; 

    [SerializeField] private Button _giftWindowActivateButton;
    [SerializeField] private Button _declineGift;
    [SerializeField] private Button _aceptGift;

    [SerializeField] private GameObject _giftWindow;
    [SerializeField] private GameObject _counter;
    [SerializeField] private GameObject _giftIcon;
    [SerializeField] private ADSManager _adsManager;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        var totalVisitors = 0;
        for (int i = 0; i < Stands.Length; i++)
        {
            if (Stands[i].IsStandBuild)
            {
                totalVisitors += VisitorCount;
            }
        }

        VisitorCount = totalVisitors;
        
        _giftWindowActivateButton.onClick.AddListener(OpenGiftWindow);
        _declineGift.onClick.AddListener(DeclineGift);
        _aceptGift.onClick.AddListener(AcceptGift);

        _giftWindow.SetActive(false);
        _giftIcon.SetActive(false);
        if (PlayerData.IsTutorialComplete)
        {
            StartCoroutine(WaitForGiftsDelay());
        }
       
        _visitorCountText.text = $"+{VisitorCount}";

        _giftBus.OnVisitorDestroy.AddListener(ChangeCounter);
        _giftBus.OnVisitorSpawn.AddListener(ChangeCounter);

        ChangeCounter();
        HideCounter();
    }

    private void Update()
    {
        _canvasGroup.alpha = (Ads.IsRewardedAdReady()) ? 1f : 0f;
        _canvasGroup.blocksRaycasts = Ads.IsRewardedAdReady();
        _canvasGroup.ignoreParentGroups = Ads.IsRewardedAdReady();
    }

    public void ShowCounter()
    {
        _counter.SetActive(true);
    }   
    
    public void HideCounter()
    {
        _counter.SetActive(false);
    }

    private void ChangeCounter()
    {
        _visitorCounter.text = $"{_giftBus.VisitorExitBus}/{VisitorCount}";
    }

    public void HideGiftWindow()
    {
        if (_giftWindow.activeSelf)
        {
            _giftWindow.SetActive(false);
        }
    }

    public void ShowGift()
    {
        HideCounter();
        StartCoroutine(WaitForGiftsDelay());
    }

    IEnumerator WaitForGiftsDelay()
    {
        yield return new WaitForSeconds(_delayBetwenGifts);

        if (!_giftIcon.activeSelf)
        {
            _giftIcon.SetActive(true);
        }
    }

    private void OpenGiftWindow()
    {
        cameraMove.TurnOffMouseCameraControl();
        _giftWindow.SetActive(true);
        _giftIcon.SetActive(false);
    }

    private void DeclineGift()
    {
        cameraMove.TurnOnMouseCameraControl();
        _giftWindow.SetActive(false);
        _giftIcon.SetActive(true);
    }

    private void AcceptGift()
    {
        cameraMove.TurnOnMouseCameraControl();
        _adsManager.ShowRewardedAds(ADSManager.RewordTypes.visitor);
        _giftWindow.SetActive(false);
    }
}

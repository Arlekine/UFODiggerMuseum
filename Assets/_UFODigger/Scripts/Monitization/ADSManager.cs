using System;
using UnityEngine;
using UnityEngine.Advertisements;
using Utils;

public class ADSManager : MonoBehaviour 
{
    [SerializeField] private PlayerExcavationTurns _playerExcavationTurns;
    [SerializeField] private Excavation _excavation;
    [SerializeField] private int _giftTurnForAds;
    [SerializeField] private Bus _bus;
    [SerializeField] private GiftController _giftController;

    public enum RewordTypes
    {
        noReward = -1,
        visitor,
        excavationTurns,
        Bomb
    };

    private RewordTypes _rewordType = RewordTypes.noReward;


    public InterstitialAd_UnityAd interstitialAd_UnityAd;
    public RewardedAd_UnityAd rewardedAd_UnityAd;

    public InterstitialAd_AdMob interstitialAd_AdMob;
    public RewardedAd_AdMob rewardedAd_AdMob;


    private void Start()
    {
        rewardedAd_UnityAd.LoadAd();
        rewardedAd_UnityAd.dSManager = this;

        interstitialAd_AdMob.dSManager = this;
        rewardedAd_AdMob.dSManager = this;
    }

    ///// SHOW REWARDED /////
    public void ShowRewardedAds(RewordTypes rewordType)
    {
        _rewordType = rewordType;

        //rewardedAd_UnityAd.ShowAd();
        rewardedAd_AdMob.ShowAd();
    }
    /////////////////////////


    ///// SHOW INTERSTITIAL /////
    public void ShowInterstitialAds()
    {
        //interstitialAd_UnityAd.ShowAd();
        interstitialAd_AdMob.ShowAd();
    }
    /////////////////////////



    public void Reward()
    {
        switch (_rewordType)
        {
            case RewordTypes.visitor:
                Debug.Log("Add people in museum");
                //AppsFlyerManager.instance.AdPlacement("Add people in museum");
                if (_bus != null)
                    _bus.StartBus();
                break;
            case RewordTypes.excavationTurns:
                Debug.Log("Add turns in excavation");
                //AppsFlyerManager.instance.AdPlacement("Add turns in excavation");
                if (_playerExcavationTurns != null)
                    _playerExcavationTurns.AddTurns(_giftTurnForAds);
                break;
            case RewordTypes.Bomb:

                Debug.Log("Add bomb");

                if (_excavation != null)
                    _excavation.SetDinamite();

                break;
            default:
                Debug.LogWarning("Nothing to reword!");
                break;
        }
    }
}
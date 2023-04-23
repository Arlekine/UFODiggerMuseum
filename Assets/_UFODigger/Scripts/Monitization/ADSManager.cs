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
    [SerializeField] private RobotButton _robotCleanerButton;

    public enum RewordTypes
    {
        noReward = -1,
        visitor,
        excavationTurns,
        Bomb,
        Robot
    };

    private RewordTypes _rewordType = RewordTypes.noReward;

    private void Start()
    {
        Ads.PreloadInterstitial();
    }

    ///// SHOW REWARDED /////
    public void ShowRewardedAds(RewordTypes rewordType)
    {
        _rewordType = rewordType;

        //rewardedAd_UnityAd.ShowAd();
        Ads.ShowRewarded("", (result) =>
        {
            if (result == AdResult.Finished) Reward();
        });
    }
    /////////////////////////


    ///// SHOW INTERSTITIAL /////
    public void ShowInterstitialAds()
    {
        //interstitialAd_UnityAd.ShowAd();
        Ads.ShowInterstitial("");
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
                {
                    _bus.StartBus();
                    Analitics.Instance.SendEvent("bus_activated");
                }

                break;
            case RewordTypes.excavationTurns:
                Debug.Log("Add turns in excavation");
                //AppsFlyerManager.instance.AdPlacement("Add turns in excavation");
                if (_playerExcavationTurns != null)
                {
                    _playerExcavationTurns.AddTurns(_giftTurnForAds);
                    Analitics.Instance.SendEvent("additional_turns_added");
                }

                break;
            case RewordTypes.Bomb:

                Debug.Log("Add bomb");

                if (_excavation != null)
                {
                    _excavation.SetDinamite();
                    Analitics.Instance.SendEvent("bomb_activated");
                }

                break;

            case RewordTypes.Robot:

                Debug.Log("Add robot");

                if (_robotCleanerButton != null)
                {
                    _robotCleanerButton.ActivateRobot();
                    Analitics.Instance.SendEvent("robot_activated");
                }

                break;
            default:
                Debug.LogWarning("Nothing to reword!");
                break;
        }
    }
}
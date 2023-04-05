using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardedAd_AdMob : MonoBehaviour
{
    private RewardedAd rewardedAd;

    [SerializeField] private string rewardedAndroid = "ca-app-pub-3940256099942544/5224354917";
    [SerializeField] private string rewardedIOS = "ca-app-pub-3940256099942544/1712485313";

    private string unityID;

    [HideInInspector] public ADSManager dSManager;

    private void OnEnable()
    {
        unityID = (Application.platform == RuntimePlatform.IPhonePlayer) ? rewardedIOS : rewardedAndroid;

        rewardedAd = new RewardedAd(unityID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(adRequest);
        rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
    }

    public void ShowAd()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            dSManager.rewardedAd_UnityAd.ShowAd();
        }
    }

    private void RewardedAd_OnUserEarnedReward(object sender, Reward e)
    {
        dSManager.Reward();
    }
}

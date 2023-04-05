using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class InterstitialAd_AdMob : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    [SerializeField] private string interstitialAndroid = "ca-app-pub-3940256099942544/1033173712";
    [SerializeField] private string interstitialIOS = "ca-app-pub-3940256099942544/4411468910";

    private string unityID;


    [HideInInspector] public ADSManager dSManager;

    private void OnEnable()
    {
        unityID = (Application.platform == RuntimePlatform.IPhonePlayer) ? interstitialIOS : interstitialAndroid;


        interstitialAd = new InterstitialAd(unityID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }


    public void ShowAd()
    {
        if(interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            dSManager.interstitialAd_UnityAd.ShowAd();
        }
    }
}

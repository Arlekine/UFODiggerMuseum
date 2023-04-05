using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;
using System;

public class AdMobManager : MonoBehaviour
{
    public static AdMobManager instance;

    [Header ("Timer Interstitial")]
    public bool _stopTimer;

    [SerializeField] private int second = 10;
    [SerializeField] private int current_seconds;
    float timer = 0.0f;

    /// Ad ///
    //public InterstitialAd _interstitialAd;

    private string interstitialUnitId = "ca-app-pub-3890298572306120/9374521434";

    bool _startDelay;

    private int _intersView_count;

    private void Awake()
    {
        if (!instance)
            instance = this;

        DontDestroyOnLoad(this);

        //MobileAds.Initialize(initStatus => { });
    }

    private void OnEnable()
    {
        ///// interstitialAd //////
        //_interstitialAd = new InterstitialAd(interstitialUnitId);
        //AdRequest adRequestInter = new AdRequest.Builder().Build();
        //_interstitialAd.LoadAd(adRequestInter);


        //// Called when an ad request has successfully loaded.
        //_interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        //// Called when an ad request failed to load.
        //_interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        //// Called when the ad is closed.
        //_interstitialAd.OnAdClosed += HandleOnAdClosed;
        ////

    }


    //public void HandleOnAdLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLoaded event received");
    //    RestartTimer();
    //}

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    MonoBehaviour.print("HandleFailedToReceiveAd");
    //    RestartTimer();
    //}

    //public void HandleOnAdClosed(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdClosed event received");
    //    RestartTimer();
    //}


    void Update()
    {
        if(!_stopTimer)
        {
            timer += Time.fixedDeltaTime;
            current_seconds = (int)(timer % 60);
            //print(current_seconds);


            if (current_seconds >= second)
            {
                _stopTimer = true;
                ShowInterstitialAd();
            }
        }

    }



    public void RestartTimer()
    {
        timer = 0;
        current_seconds = 0;
        _stopTimer = false;

        Debug.Log("Restart Timer InterstitialAd");
    }


    public void ShowInterstitialAd()
    {
        _intersView_count++;

        //if(_intersView_count >= 3)
        //{
        //    _intersView_count = 0;

        //    if (_interstitialAd.IsLoaded())
        //    {
        //        _interstitialAd.Show();
        //        Debug.Log("Show InterstitialAd");
        //    }
        //    else
        //    {
        //        if (!_startDelay)
        //            StartCoroutine(DelayRestart());

        //        Debug.Log("Error InterstitialAd");
        //    }

        //    StartCoroutine(DelayRestart());
        //}
        //else
        //{
        //    RestartTimer();
        //}
    }


    IEnumerator DelayRestart()
    {
        _startDelay = true;

        yield return new WaitForSeconds(5);

        if(_stopTimer)
            RestartTimer();

        _startDelay = false;
    }

}

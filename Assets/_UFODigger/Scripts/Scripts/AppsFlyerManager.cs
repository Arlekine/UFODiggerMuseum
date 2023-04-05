using System.Collections;
using System.Collections.Generic;
//using AppsFlyerSDK;
using UnityEngine;

public class AppsFlyerManager : MonoBehaviour
{
    public static AppsFlyerManager instance;

    private const int SecondsPerMinute = 60;

    private double _totalTime;
    private float _prevTime;

    string _currentTime;
    int _currentMun;

    bool _activeTimer;

    private void Awake()
    {
        if (!instance)
            instance = this;

        DontDestroyOnLoad(this);
    }


    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            EndSession();
        }
        else
        {
            StartSession();
        }
    }

    private void Update()
    {
        if (!_activeTimer) return;

        var current = Time.realtimeSinceStartup;
        var dt = current - _prevTime;
        _prevTime = current;

        _totalTime += dt;

        ReportMinutes();
    }

    private void ReportMinutes()
    {
        if (_totalTime >= SecondsPerMinute)
        {
            _totalTime = 0;
            _currentMun++;
        }

        var minutes = (int)(_totalTime / SecondsPerMinute);
        var second = (int)_totalTime;

        _currentTime = _currentMun + ":" + second;

        //Debug.Log($"<color=yellow>ReportMinutes</color> OnMinutePassed minutes = {_currentTime}");
    }

    public void StartSession()
    {
        _activeTimer = true;
        Debug.Log($"<color=green>StartSession</color>");
    }
    public void EndSession()
    {
        _activeTimer = false;
        LevelTime(_currentTime);
        Debug.Log($"<color=red>EndSession: </color>" + _currentTime);
    }


    public void LevelTime(string timer)
    {
        Dictionary<string, string> purchaseEvent = new Dictionary<string, string>();
        purchaseEvent["timer"] = timer;

        //AppsFlyer.sendEvent("timer", purchaseEvent);

        //Debug.Log("timer: " + timer);
    }


    //public void LevelProgress(int _levelNum, string _status)
    //{
    //    Dictionary<string, string> progressEvent = new
    //    Dictionary<string, string>();

    //    progressEvent.Add("level", _levelNum.ToString());
    //    progressEvent.Add("status", _status);

    //    //AppsFlyer.sendEvent("progress", progressEvent);


    //    Debug.Log("level:  " + _levelNum.ToString() + "   status: " + _status);
    //}


    //public void TutorialState(string _step)
    //{
    //    Dictionary<string, string> tutorialEvent = new
    //    Dictionary<string, string>();

    //    tutorialEvent.Add("Step", _step);

    //    //AppsFlyer.sendEvent("Tutorial", tutorialEvent);


    //    Debug.Log("Tutorial Step: " + _step);
    //}

    //public void TutorialFinish()
    //{
    //    Dictionary<string, string> tutorial_finishEvent = new
    //    Dictionary<string, string>();

    //    tutorial_finishEvent.Add("", "");

    //    //AppsFlyer.sendEvent("tutorial_finish", tutorial_finishEvent);

    //    Debug.Log("TUTORIAL FINISHED");
    //}


    //public void AdPlacement(string _ad_placement)
    //{
    //    Dictionary<string, string> rvfinishEvent = new
    //    Dictionary<string, string>();

    //    rvfinishEvent.Add("placement", _ad_placement);

    //    //AppsFlyer.sendEvent("rv_finish", rvfinishEvent);

    //    Debug.Log("placement: " + _ad_placement);
    //}


    //public void Purchase(string productId, string transactionId, string revenue, string currency)
    //{
    //    Dictionary<string, string> purchaseEvent = new
    //    Dictionary<string, string>();

    //    //purchaseEvent.Add(AFInAppEvents.CURRENCY, currency);
    //    //purchaseEvent.Add(AFInAppEvents.REVENUE, revenue);
    //    //purchaseEvent.Add(AFInAppEvents.QUANTITY, "1");
    //    //purchaseEvent.Add("SKU", productId);
    //    //purchaseEvent.Add("TRANSACTION_ID", transactionId);


    //    //AppsFlyer.sendEvent("af_purchase", purchaseEvent);


    //    Debug.Log("af_purchase: " + revenue + "  " + currency + "  SKU: " + productId + "    TRANSACTION_ID: " + transactionId);
    //}


    //public void UniquePurchase()
    //{
    //    Dictionary<string, string> uniquepuEvent = new
    //    Dictionary<string, string>();

    //    uniquepuEvent.Add("revenue", "");

    //    //AppsFlyer.sendEvent("unique_pu", uniquepuEvent);

    //    Debug.Log("unique_pu: " + uniquepuEvent);
    //}

}

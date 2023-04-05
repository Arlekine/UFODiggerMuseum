using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMetricaManager : MonoBehaviour
{
    public static AppMetricaManager instance;

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
        var parameters = new Dictionary<string, object>();
        parameters["timer"] = timer;

        //AppMetrica.Instance.ReportEvent("Timer: ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();

        Debug.Log("timer: " + timer);
    }

    public void LevelProgress(int _levelNum, string _status)
    {
        var parameters = new Dictionary<string, object>();
        parameters["level"] = _levelNum.ToString();
        parameters["status"] = _status;

        //AppMetrica.Instance.ReportEvent("Level Progress: ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();


        Debug.Log("level:  " + _levelNum.ToString() + "   status: " + _status);
    }



    ///  TUTORIAL ///
    public void TutorialState(string _step)
    {

        var parameters = new Dictionary<string, object>();
        parameters["Step"] = _step;

        //AppMetrica.Instance.ReportEvent("Tutorial Step: ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();


        Debug.Log("Tutorial Step: " + _step);
    }

    public void TutorialFinish()
    {
        var parameters = new Dictionary<string, object>();
        parameters["finish"] = "";

        //AppMetrica.Instance.ReportEvent("Tutorial Finish: ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();

        Debug.Log("TUTORIAL FINISHED");
    }
    //////////////////
    


    /// AD PLACEMENT ///
    public void AdPlacement(string _ad_placement)
    {
        var parameters = new Dictionary<string, object>();
        parameters["placement"] = _ad_placement;

        //AppMetrica.Instance.ReportEvent("AdPlacement : ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();

        Debug.Log("placement: " + _ad_placement);
    }


    public void Purchase(string productId, string transactionId, string revenue, string currency)
    {
        var parameters = new Dictionary<string, object>();
        parameters["CURRENCY"] = currency;
        parameters["REVENUE"] = revenue;
        parameters["QUANTITY"] = "1";
        parameters["SKU"] = productId;
        parameters["TRANSACTION_ID"] = transactionId;

        //AppMetrica.Instance.ReportEvent("AF Purchase: ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();


        Debug.Log("af_purchase: " + revenue + "  " + currency + "  SKU: " + productId + "    TRANSACTION_ID: " + transactionId);
    }

    public void UniquePurchase()
    {
        var parameters = new Dictionary<string, object>();
        parameters["revenue"] = "";

        //AppMetrica.Instance.ReportEvent("Unique Purchase : ", parameters: parameters);
        //AppMetrica.Instance.SendEventsBuffer();
    }
    /////////////////
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdInitialization : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string androidGameID;
    [SerializeField] string iOSGameID;

    [SerializeField] bool testMode;
    private string gameId;


    private void Awake()
    {
        InitializeAd();
    }

    public void InitializeAd()
    {
        Debug.Log("Unity Ads initialization started.");

        gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSGameID
            : androidGameID;

        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization Complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Unity Ads Initialization Failed");
    }
}

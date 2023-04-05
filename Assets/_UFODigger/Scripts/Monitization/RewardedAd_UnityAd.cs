using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAd_UnityAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdID = "Rewarded_Android";
    [SerializeField] private string iosdAdID = "Rewarded_iOS";

    private string adID;

    [HideInInspector] public ADSManager dSManager;


    private void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosdAdID : androidAdID;

    }

    private void Start()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + adID);
        Advertisement.Load(adID, this);
    }

    public void ShowAd()
    {
        Debug.Log("Show Ad: " + adID);
        Advertisement.Show(adID, this);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Loaded Ad: " + placementId);

        if(placementId.Equals(adID))
        {

        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            dSManager.Reward();
        }
        else if (showCompletionState == UnityAdsShowCompletionState.SKIPPED)
        {
            Debug.Log("ads was skipped");
        }
        else if (showCompletionState == UnityAdsShowCompletionState.UNKNOWN)
        {
            Debug.Log("Ads error");
        }
    }
}

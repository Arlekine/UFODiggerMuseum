using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdInitialization_AdMob : MonoBehaviour
{

    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}

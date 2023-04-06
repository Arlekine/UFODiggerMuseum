using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGiftButton : MonoBehaviour
{
    [SerializeField] private ADSManager _adsManager;
    [SerializeField] private GameObject _giftWindow;

    public void OpenAcceptionPanel()
    {
        _giftWindow.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DeclineGift()
    {
        _giftWindow.SetActive(false);
        gameObject.SetActive(true);
    }

    public void AcceptGift()
    {
        _adsManager.ShowRewardedAds(ADSManager.RewordTypes.Bomb);
        _giftWindow.SetActive(false);
    }
}

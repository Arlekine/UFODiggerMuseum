using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnGiftController : MonoBehaviour
{
    [SerializeField] private Button _declineGift;
    [SerializeField] private Button _aceptGift;
    [SerializeField] private PlayerExcavationTurns _playerExcavationTurns;

    [SerializeField] private int _giftTurnForAds;
    [SerializeField] private ADSManager _adsManager;

    private void Start()
    {
        _declineGift.onClick.AddListener(DeclineGift);
        _aceptGift.onClick.AddListener(AcceptGift);
    }

    private void DeclineGift()
    {
        gameObject.SetActive(false);

        if (_playerExcavationTurns._playerTurns <= 0)
        {
            _playerExcavationTurns.EndExcavate();
        }
    }

    private void AcceptGift()
    {
        _adsManager.ShowRewardedAds(ADSManager.RewordTypes.excavationTurns);
        gameObject.SetActive(false);
    }
}

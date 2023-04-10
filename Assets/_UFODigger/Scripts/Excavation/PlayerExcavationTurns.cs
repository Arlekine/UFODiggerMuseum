using System;
using System.Collections;
using UnityEngine;

public class PlayerExcavationTurns : MonoBehaviour
{
    public FoundPart FoundPart;
    public ExcavateComplete ExcavateComplete;
    public UpgradesSO PlayerTurns;
    
    [SerializeField] private PLayerData _playerData;
    [SerializeField] private PlayerTurnsUI _playerTurnsUI;
    [SerializeField] private Excavation _excavation;
    [SerializeField] private GameObject _giftButton;

    [SerializeField] private StaminaDrinkSo _staminaDrinkSo;
    [SerializeField] private UISkillsBag _skillsBag;

    public int _playerTurns;
    private int _maxTurns;
    public bool _offerAd;

    public bool CanExcavate { get; private set; }

    private void Start()
    {
        FoundPart.AllPartFound += BlockTurns;
        _maxTurns = _playerData.ExcavationTurns;
        _playerTurns = PlayerTurns.GetTotalPower();
        CanExcavate = true;
    }

    private void BlockTurns()
    {
        CanExcavate = false;
    }

    public void Excavate()
    {
        _playerTurnsUI.UpdateTurnsUI();
        _playerTurns -= 1;

        if (_playerTurns <= 0)
        {
            if(_playerTurnsUI._wasADSShow)
            {
                EndExcavate();
            }
            else
            {
                BlockTurns();
                StartCoroutine(ShowGiftOfferDelay());
            }
        }
    }

    public void EndExcavate()
    {
        _giftButton.SetActive(false);
        ExcavateComplete.CompleteExcavate();
        CanExcavate = false;
    }

    public void AddTurns(int turns, bool fromskill = false)
    {
        _playerTurns += turns;
        _playerTurnsUI.AddTurns(turns, fromskill);
        CanExcavate = true;

        if (fromskill)
        {
            StartCoroutine(RemoveSkillFromBag());
        }
    }

    IEnumerator ShowGiftOfferDelay()
    {
        yield return new WaitForSeconds(0.5f);

        if (ExcavateComplete.AlienData.FoundedParts != null && AlienData.FoundedParts.Count != 2)
            _playerTurnsUI.ShowGiftOffer();
    }

    IEnumerator RemoveSkillFromBag()
    {
        yield return new WaitForSeconds(2f);

        _skillsBag.RemoveSkill(_staminaDrinkSo as ISkill);
    }
}

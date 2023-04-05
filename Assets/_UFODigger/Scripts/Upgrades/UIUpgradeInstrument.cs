using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIUpgradeInstrument : MonoBehaviour
{
    public bool IsGoldPrice;

    public InstrumentData Instrument;
    public Button BuyButton;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI[] PowerUP;
    public TextMeshProUGUI UpgradeLevelText;
    public Image InstructionIcon;
    public Image InstrumentIcon;
    public UIInfo UIInfo;
    private string _price;
    
    private void Start()
    {
        Instrument.LoadData();
        SetUpgradeInfo();
        Instrument.OnLevelRise.AddListener(SetUpgradeInfo);
        BuyButton.onClick.AddListener(BuyUpgrade);

        InstructionIcon.sprite = Instrument.InstrumentPowerDescription;
        InstrumentIcon.sprite = Instrument.Icon;
    }

    private void SetUpgradeInfo()
    {
        if (!Instrument.IsInstrumentUnlock)
        {
            UpgradeLevelText.text =$"Buy for unlock";
            Price.text = Instrument.UnlockInstrumentPrice.ToString();
            _price =  Instrument.UnlockInstrumentPrice.ToString();
            for (var i = 0; i < PowerUP.Length; i++)
            {
                PowerUP[i].text = $""; 
            }
            return;
        }

        if (Instrument.CheckIfMaxLevel())
        {
            BuyButton.gameObject.SetActive(false);
            UpgradeLevelText.text =$"lvl: {Instrument.LevelOfUpgrade+1} MAX";
            
            for (int i = 0; i < PowerUP.Length; i++)
            {
                PowerUP[i].text = $"{Instrument.Upgrades[Instrument.LevelOfUpgrade].Power[i]}"; 
            }
            
            BuyButton.onClick.RemoveListener(BuyUpgrade);
        }
        else
        {
            if (IsGoldPrice)
            {
                Price.text = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GoldPrice.ToString();
                _price = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GoldPrice.ToString();
            }
            else
            {
                Price.text = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GemPrice.ToString();
                _price = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GemPrice.ToString();
            }

           
            for (int i = 0; i < PowerUP.Length; i++)
            {
                PowerUP[i].text = $"{Instrument.Upgrades[Instrument.LevelOfUpgrade].Power[i]}"; 
            }
            
            UpgradeLevelText.text =$"lvl: {Instrument.LevelOfUpgrade + 1 }/{Instrument.Upgrades.Length}";
           
        }
    }

    private void BuyUpgrade()
    {
        if (!Instrument.IsInstrumentUnlock)
        {
            Instrument.UnlockInstrument();
            return;
        }

        if (IsGoldPrice)
        {
            Instrument.RiseLevelByGold();
        }
        else
        {
            Instrument.RiseLevelByGems();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UIUpgradeMuseum : MonoBehaviour
{
    public string UpgradeSymbol;
    public UpgradesSO UpgradeSo;
    public Button BuyButton;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI Power;
    public TextMeshProUGUI PowerUP;
    public TextMeshProUGUI UpgradeLevelText;
    [FormerlySerializedAs("UIUpgradeInfo")] public UIInfo UIInfo;

    private string _price;
    private void Start()
    {
        UpgradeSo.LoadData();
        SetUpgradeInfo();
        UpgradeSo.OnLevelRise.AddListener(SetUpgradeInfo);
        BuyButton.onClick.AddListener(BuyUpgrade);
    }

    private void SetUpgradeInfo()
    {
        if (UpgradeSo.CheckIfMaxLevel())
        {
            BuyButton.gameObject.SetActive(false);
            UpgradeLevelText.text =$"lvl: {UpgradeSo.Level+1} MAX";
            Power.text = $"{UpgradeSo.GetTotalPower()}{UpgradeSymbol}";
          
            PowerUP.text = $"";
            BuyButton.onClick.RemoveListener(BuyUpgrade);
        }
        else
        {
            _price = UpgradeSo.Levels[UpgradeSo.Level + 1].GoldCost.ToString();
            Price.text = _price;
            Power.text = $"{UpgradeSo.GetTotalPower()}{UpgradeSymbol}";
            PowerUP.text = $"+{UpgradeSo.Levels[UpgradeSo.Level + 1].PowerUP}{UpgradeSymbol}";
            UpgradeLevelText.text =$"lvl: {UpgradeSo.Level+1}/{UpgradeSo.Levels.Length}";
        }
    }

    private void BuyUpgrade()
    {
        UpgradeSo.RiseLevelByGold(); 
    }
}

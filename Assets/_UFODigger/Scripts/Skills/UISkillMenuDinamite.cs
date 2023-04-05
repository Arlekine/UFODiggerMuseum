using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UISkillMenuDinamite : MonoBehaviour
{
    public DinamiteData Dinamite;

    public Button BuyButton;
    public Button UpgradeButton;

    public TextMeshProUGUI PriceUpgrade;
    public TextMeshProUGUI PriceBuy;

    public TextMeshProUGUI PowerUP;
    public TextMeshProUGUI Power;

    public TextMeshProUGUI SkillLevelText;
    public TextMeshProUGUI SkillCountText;

    public Image InstrumentIcon;

    private void Start()
    {
        Dinamite.LoadData();
        Dinamite.LoadSkillData();
        SetUpSkillInfo();
        Dinamite.OnLevelRise.AddListener(SetUpSkillInfo);
        Dinamite.OnDinamiteBuy.AddListener(SetUpSkillInfo);

        BuyButton.onClick.AddListener(BuySkill);
        UpgradeButton.onClick.AddListener(UpgradeSkill);

        InstrumentIcon.sprite = Dinamite.Icon;
    }

    private void SetUpSkillInfo()
    {
        if (Dinamite.CheckIfMaxLevel())
        {
            UpgradeButton.gameObject.SetActive(false);
            SkillLevelText.text = $"lvl: {Dinamite.LevelOfUpgrade + 1} MAX";

            PowerUP.text = $"";
            Power.text = $"{Dinamite.Upgrades[Dinamite.LevelOfUpgrade].Power[0]}";

            BuyButton.onClick.RemoveListener(UpgradeSkill);
        }
        else
        {
            PriceUpgrade.text = Dinamite.Upgrades[Dinamite.LevelOfUpgrade + 1].GemPrice.ToString();

            var powerNow = Dinamite.Upgrades[Dinamite.LevelOfUpgrade].Power[0];
            var powerUp = Dinamite.Upgrades[Dinamite.LevelOfUpgrade + 1].Power[0];
            Power.text = $"{powerNow}";
            PowerUP.text = $"+{powerUp - powerNow}";
            SkillLevelText.text = $"lvl: {Dinamite.LevelOfUpgrade + 1}/{Dinamite.Upgrades.Length}";
        }


        SkillCountText.text = $"Count: {Dinamite.DinamitCount}";
        PriceBuy.text = $"{Dinamite.BuyGemCost}";
    }

    private void UpgradeSkill()
    {
        Dinamite.RiseLevelByGems();
    }

    private void BuySkill()
    {
        Dinamite.BuySkill();
    }
}
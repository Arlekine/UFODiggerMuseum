using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillMenuStaminaDrink : MonoBehaviour
{
    public StaminaDrinkSo StaminaDrink;
    
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
        StaminaDrink.LoadSkillData();
        SetUpSkillInfo();
        StaminaDrink.OnSkillChange.AddListener(SetUpSkillInfo);

        BuyButton.onClick.AddListener(BuySkill);
        UpgradeButton.onClick.AddListener(UpgradeSkill);

        InstrumentIcon.sprite = StaminaDrink.SkillImage;
    }
    private void SetUpSkillInfo()
    {
        SkillCountText.text = $"Count: {StaminaDrink.DrinkCount}";
        PriceBuy.text = $"{StaminaDrink.BuyGemCost}";
        
        if (StaminaDrink.CheckIfMaxLevel())
        {
            UpgradeButton.gameObject.SetActive(false);
            SkillLevelText.text = $"lvl: {StaminaDrink.LevelOfSkill + 1} MAX";

            PowerUP.text = $"";
            Power.text = $"{StaminaDrink.StaminaDrinkLevels[StaminaDrink.LevelOfSkill].StaminaRecover}";

            BuyButton.onClick.RemoveListener(UpgradeSkill);
        }
        else
        {
            PriceUpgrade.text = StaminaDrink.StaminaDrinkLevels[StaminaDrink.LevelOfSkill + 1].UpgradeGemCost.ToString();

            var powerNow = StaminaDrink.StaminaDrinkLevels[StaminaDrink.LevelOfSkill].StaminaRecover;
            var powerUp = StaminaDrink.StaminaDrinkLevels[StaminaDrink.LevelOfSkill + 1].StaminaRecover;
            
            Power.text = $"{powerNow}";
            PowerUP.text = $"+{powerUp - powerNow}";
            SkillLevelText.text = $"lvl: {StaminaDrink.LevelOfSkill + 1}/{StaminaDrink.StaminaDrinkLevels.Length}";
        }
    }
    private void UpgradeSkill()
    {
        StaminaDrink.RiseLevelByGems();
    }

    private void BuySkill()
    {
        StaminaDrink.BuySkill();
    }


}

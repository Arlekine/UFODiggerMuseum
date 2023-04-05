using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillMenuSuperLight : MonoBehaviour
{
     public SuperLightSO SuperLight;
    
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
        SuperLight.LoadSkillData();
        SetUpSkillInfo();
        SuperLight.OnSkillChange.AddListener(SetUpSkillInfo);

        BuyButton.onClick.AddListener(BuySkill);
        UpgradeButton.onClick.AddListener(UpgradeSkill);

        InstrumentIcon.sprite = SuperLight.SkillImage;
    }
    private void SetUpSkillInfo()
    {
        SkillCountText.text = $"Count: {SuperLight.SuperLightCount}";
        PriceBuy.text = $"{SuperLight.BuyGemCost}";
        
        if (SuperLight.CheckIfMaxLevel())
        {
            UpgradeButton.gameObject.SetActive(false);
            SkillLevelText.text = $"lvl: {SuperLight.LevelOfSkill + 1} MAX";

            PowerUP.text = $"";
            Power.text = $"{SuperLight.SuperLightLevels[SuperLight.LevelOfSkill].IncomUp}";

            BuyButton.onClick.RemoveListener(UpgradeSkill);
        }
        else
        {
            PriceUpgrade.text = SuperLight.SuperLightLevels[SuperLight.LevelOfSkill + 1].UpgradeGemCost.ToString();

            var powerNow = SuperLight.SuperLightLevels[SuperLight.LevelOfSkill].IncomUp;
            var powerUp = SuperLight.SuperLightLevels[SuperLight.LevelOfSkill+1].IncomUp;
            
            Power.text = $"{powerNow}";
            PowerUP.text = $"+{powerUp - powerNow}";
            SkillLevelText.text = $"lvl: {SuperLight.LevelOfSkill + 1}/{SuperLight.SuperLightLevels.Length}";
        }
        
    }
    private void UpgradeSkill()
    {
        SuperLight.RiseLevelByGems();
    }

    private void BuySkill()
    {
        SuperLight.BuySkill();
    }

}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkillMenuAlienDetector : MonoBehaviour
{

    public FossilDetectorSO AlienDetector;
    
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
        AlienDetector.LoadSkillData();
        SetUpSkillInfo();
        AlienDetector.OnSkillChange.AddListener(SetUpSkillInfo);

        BuyButton.onClick.AddListener(BuySkill);
        UpgradeButton.onClick.AddListener(UpgradeSkill);

        InstrumentIcon.sprite = AlienDetector.SkillImage;
    }
    private void SetUpSkillInfo()
    {
        SkillCountText.text = $"Count: {AlienDetector.DetectorsCount}";
        PriceBuy.text = $"{AlienDetector.BuyGemCost}";
        
        if (AlienDetector.CheckIfMaxLevel())
        {
            UpgradeButton.gameObject.SetActive(false);
            SkillLevelText.text = $"lvl: {AlienDetector.LevelOfSkill + 1} MAX";

            PowerUP.text = $"";
            Power.text = $"{AlienDetector.DetectorLevels[AlienDetector.LevelOfSkill].Accuracy}";

            BuyButton.onClick.RemoveListener(UpgradeSkill);
        }
        else
        {
            PriceUpgrade.text = AlienDetector.DetectorLevels[AlienDetector.LevelOfSkill + 1].UpgradeGemCost.ToString();

            var powerNow = AlienDetector.DetectorLevels[AlienDetector.LevelOfSkill].Accuracy;
            var powerUp = AlienDetector.DetectorLevels[AlienDetector.LevelOfSkill + 1].Accuracy;
            
            Power.text = $"{powerNow}";
            PowerUP.text = $"+{powerUp - powerNow}";
            SkillLevelText.text = $"lvl: {AlienDetector.LevelOfSkill + 1}/{AlienDetector.DetectorLevels.Length}";
        }
        
    }
    private void UpgradeSkill()
    {
        AlienDetector.RiseLevelByGems();
    }

    private void BuySkill()
    {
        AlienDetector.BuySkill();
    }


}

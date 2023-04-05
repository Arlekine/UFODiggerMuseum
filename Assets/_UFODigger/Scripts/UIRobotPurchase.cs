using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class UIRobotPurchase : MonoBehaviour
{
    public bool IsGoldPrice;
    //public PurchasesManager PurchasesManager;
    public GameObject GemIcon;
    private const string _robot = "robot";
    
    public InstrumentData Instrument;
    public Button BuyButton;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI[] PowerUP;
    public TextMeshProUGUI UpgradeLevelText;
    public Image InstructionIcon;
    public Image InstrumentIcon;
    public UIInfo UIInfo;
    
    private string _price;

    private void Awake()
    {
        //PurchasesManager.RobotMetadataUpdated += UpdateRobotPurchase;
    }

    private void UpdateRobotPurchase(ProductMetadata productMeta)
    {
        Price.text = $"{productMeta.isoCurrencyCode} {productMeta.localizedPrice:0.##}";
        _price = $"{productMeta.isoCurrencyCode} {productMeta.localizedPrice:0.##}";
        SetUpgradeInfo();
    }

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
            GemIcon.SetActive(false);
            for (var i = 0; i < PowerUP.Length; i++)
            {
                PowerUP[i].text = $""; 
            }
            return;
        }

        BuyButton.gameObject.SetActive(false);

        //GemIcon.SetActive(true);

        //if (Instrument.CheckIfMaxLevel())
        //{
        //    BuyButton.gameObject.SetActive(false);
        //    UpgradeLevelText.text =$"lvl: {Instrument.LevelOfUpgrade+1} MAX";

        //    for (int i = 0; i < PowerUP.Length; i++)
        //    {
        //        PowerUP[i].text = $"{Instrument.Upgrades[Instrument.LevelOfUpgrade].Power[i]}"; 
        //    }

        //    BuyButton.onClick.RemoveListener(BuyUpgrade);
        //}
        //else
        //{
        //    if (IsGoldPrice)
        //    {
        //        Price.text = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GoldPrice.ToString();
        //        _price = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GoldPrice.ToString();
        //    }
        //    else
        //    {
        //        Price.text = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GemPrice.ToString();
        //        _price = Instrument.Upgrades[Instrument.LevelOfUpgrade+1].GemPrice.ToString();
        //    }


        //    for (int i = 0; i < PowerUP.Length; i++)
        //    {
        //        PowerUP[i].text = $"{Instrument.Upgrades[Instrument.LevelOfUpgrade].Power[i]}"; 
        //    }

        //    UpgradeLevelText.text =$"lvl: {Instrument.LevelOfUpgrade + 1 }/{Instrument.Upgrades.Length}";

        //}
    }

    public void UnlockRobot()
    {
        Instrument.UnlockInstrumentBuyUsd();
    }

    private void BuyUpgrade()
    {
        //if (!Instrument.IsInstrumentUnlock)
        //{
        //    PurchasesManager.BuyProductID(_robot);
        //    return;
        //}

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

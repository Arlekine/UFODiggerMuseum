using System;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OfflineIncome : MonoBehaviour
{
    public int GoldByMinute;
    public int MaximumOfflineIncome;
    public int MinimumOfflineIncome;

    public UIGoldMove UIGoldMove;
    
    public PLayerData PlayerData;

    public GameObject OfflineIncomeObject;
    public Button GetIncomeButton;
    public TextMeshProUGUI IncomeCountText;

    private int _income;
    
    private void Start()
    {
        if (!PlayerData.IsTutorialComplete)
        {
            OfflineIncomeObject.SetActive(false);
            return;
        }

        if (PlayerData.OfflineTime != TimeSpan.Zero || PlayerData.AbandonedGold>0)
        {
            var totalOfflineMinutes = PlayerData.OfflineTime.TotalMinutes;
            _income = GoldByMinute * (int)totalOfflineMinutes + PlayerData.AbandonedGold;
            Debug.Log("_income - " + _income);
            if (_income > MinimumOfflineIncome)
            {
                GetIncomeButton.onClick.AddListener(GetIncome); 
                OfflineIncomeObject.SetActive(true);
                if (_income > MaximumOfflineIncome)
                {
                    _income = MaximumOfflineIncome;
                }
                IncomeCountText.text = "+"+_income.ToString();
            }
            else
            {
                OfflineIncomeObject.SetActive(false);
            }
        }
        else
        {
            OfflineIncomeObject.SetActive(false);
        }
    }

    private void GetIncome()
    {
        if (UISettingsVibro._vibroStatus)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
        }
        UIGoldMove.MoveGold();
        PlayerData.AddGold(_income);
        OfflineIncomeObject.SetActive(false);
    }
}

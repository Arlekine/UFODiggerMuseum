using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "Instrument", menuName = "ScriptableObjects/Instrument", order = 4)]
public class InstrumentData : ScriptableObject
{
    public int StartLevelOfUpgrade;
    public UnityEvent OnLevelRise;
    public PLayerData PLayerData;
    public int LevelOfUpgrade;
    public Sprite InstrumentPowerDescription;
    public Sprite Icon;
    public InstrimentUpgrade[] Upgrades;

    public int UnlockInstrumentPrice;
    public bool IsInstrumentUnlockOnStart;
    public bool IsInstrumentUnlock;

    public string SaveKey;
    public string SaveLockKey;

    [TextArea] public string Description;

    public bool CheckIfMaxLevel()
    {
        return (LevelOfUpgrade >= Upgrades.Length - 1);
    }

    public void UnlockInstrumentBuyUsd()
    {
        IsInstrumentUnlock = true;
        OnLevelRise.Invoke();
        SaveData();
    }

    public void UnlockInstrument()
    {
        if (PLayerData.GemCount >= UnlockInstrumentPrice)
        {
            PLayerData.AddGems(-UnlockInstrumentPrice);
            IsInstrumentUnlock = true;
            OnLevelRise.Invoke();
        }

        SaveData();
    }

    public void RiseLevelByGems()
    {
        if (LevelOfUpgrade >= Upgrades.Length - 1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PLayerData.GemCount >= Upgrades[LevelOfUpgrade + 1].GemPrice)
        {
            LevelOfUpgrade++;
            PLayerData.AddGems(-Upgrades[LevelOfUpgrade].GemPrice);
            OnLevelRise.Invoke();
        }

        SaveData();
    }

    public void RiseLevelByGold()
    {
        if (LevelOfUpgrade >= Upgrades.Length - 1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PLayerData.GemCount >= Upgrades[LevelOfUpgrade + 1].GoldPrice)
        {
            LevelOfUpgrade++;
            PLayerData.AddGems(-Upgrades[LevelOfUpgrade].GoldPrice);
            OnLevelRise.Invoke();
        }

        SaveData();
    }

    public void LoadData()
    {
        if (SaveLoadSystem.CheckKey(SaveKey))
        {
            LevelOfUpgrade = SaveLoadSystem.LoadInt(SaveKey);
        }
        else
        {
            LevelOfUpgrade = StartLevelOfUpgrade;
        }

        if (SaveLoadSystem.CheckKey(SaveLockKey))
        {
            IsInstrumentUnlock = SaveLoadSystem.LoadBool(SaveLockKey);
        }
        else
        {
            IsInstrumentUnlock = IsInstrumentUnlockOnStart;
        }
    }

    public void SaveData()
    {
        SaveLoadSystem.Save(SaveKey, LevelOfUpgrade);
        SaveLoadSystem.Save(SaveLockKey, IsInstrumentUnlock);
    }
}

[Serializable]
public class InstrimentUpgrade
{
    public int GoldPrice;
    public int GemPrice;

    // [Header("Alien who needs to be open to unlock this upgrade ")]
    // public Alien AlienForUnlock;

    [Header("Power of digging")] public int[] Power;
}
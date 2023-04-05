using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade", order = 4)]
public class UpgradesSO : ScriptableObject
{
    public int Level { get; private set; }
    public int StartPower;
    public UnityEvent OnLevelRise;
    public PLayerData PLayerData;
    public UpgradeLevel[] Levels;

    public string SaveKey;
    [TextArea]
    public string Description;
    public int GetTotalPower()
    {
        var totalPower = StartPower;
        for (int i = 0; i <= Level; i++)
        {
            totalPower += Levels[i].PowerUP;
        }

        return totalPower;
    }

    public bool CheckIfMaxLevel()
    {
        return (Level >= Levels.Length-1);
    }

    public void RiseLevelByGems()
    {
        if (Level >= Levels.Length-1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PLayerData.GemCount >= Levels[Level+1].GemCost)
        {
            Level++;
            PLayerData.AddGems(-Levels[Level].GemCost);
            OnLevelRise.Invoke();
        }

        SaveData();
    }

    public void RiseLevelByGold()
    {
        if (Level >= Levels.Length-1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PLayerData.GoldCount >= Levels[Level+1].GoldCost)
        {
            Level++;
            Debug.Log(Level + " Level");
            PLayerData.AddGold(-Levels[Level].GoldCost);
            OnLevelRise.Invoke();
        }

        SaveData();
    }

    public void LoadData()
    {
        if (SaveLoadSystem.CheckKey(SaveKey))
        {
            Level = SaveLoadSystem.LoadInt(SaveKey);
        }
        else
        {
            Level = 0;
        }
    }

    public void SaveData()
    {
        SaveLoadSystem.Save(SaveKey, Level);
    }

    [Serializable]
    public class UpgradeLevel
    {
        public int GoldCost;
        public int GemCost;
        public int PowerUP;
    }
}
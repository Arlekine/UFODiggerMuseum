using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "StaminaDrink", menuName = "ScriptableObjects/Skills/StaminaDrink", order = 4)]
public class StaminaDrinkSo : ScriptableObject, ISkill
{
    public PLayerData PlayerData;
    public int DrinkCount;
    public int LevelOfSkill;
    public int BuyGemCost;
    public Sprite SkillImage;
    public StaminaDrink[] StaminaDrinkLevels; 
    public string DrinkDescription;
    
    public string SaveKeyCount;
    public string SaveKeyLevel;
    public UnityEvent OnSkillChange;
    
    [TextArea] public string Info;
    
    public int Level
    {
        get { return LevelOfSkill; }
        set { LevelOfSkill = value; }
    }
    public int Count
    {
        get { return DrinkCount; }
        set { DrinkCount = value; }
    }

    public string Description
    {
        get { return DrinkDescription; }
        set { DrinkDescription = value; }
    }

    public Sprite SkillSprite
    {
        get { return SkillImage; }
        set { SkillImage = value; }
    }

    public void Use()
    {
        if (DrinkCount < 1)
            return;

        var playerTurns = FindObjectOfType<PlayerExcavationTurns>();

        if (playerTurns != null)
        {
            DrinkCount--;
            playerTurns.AddTurns(StaminaDrinkLevels[LevelOfSkill].StaminaRecover,true);
        }

        SaveSkillData();
    }

    public void RiseLevelByGems()
    {
        if (LevelOfSkill >= StaminaDrinkLevels.Length -1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PlayerData.GemCount >= StaminaDrinkLevels[LevelOfSkill + 1].UpgradeGemCost)
        {
            LevelOfSkill++;
            PlayerData.AddGems(-StaminaDrinkLevels[LevelOfSkill].UpgradeGemCost);
            OnSkillChange.Invoke();
        }

        SaveSkillData();
    }
    public bool CheckIfMaxLevel()
    {
        return (LevelOfSkill >= StaminaDrinkLevels.Length - 1);
    }
    
    public void SaveSkillData()
    {
        SaveLoadSystem.Save(SaveKeyCount,DrinkCount);
        SaveLoadSystem.Save(SaveKeyLevel,LevelOfSkill);
    }

    public void LoadSkillData()
    {
        if (SaveLoadSystem.CheckKey(SaveKeyCount))
        {
            DrinkCount =  SaveLoadSystem.LoadInt(SaveKeyCount);
        }
        else
        {
            DrinkCount = 0;
        }
        
        if (SaveLoadSystem.CheckKey(SaveKeyLevel))
        {
            LevelOfSkill =  SaveLoadSystem.LoadInt(SaveKeyLevel);
        }
        else
        {
            LevelOfSkill = 0;
        }
    }

    public void BuySkill()
    {
        if (PlayerData.GemCount >= BuyGemCost)
        {
            PlayerData.AddGems(-BuyGemCost);
            Count++;
            SaveSkillData();
            OnSkillChange.Invoke();
        }
    }
}

[Serializable]
public class StaminaDrink 
{
    public int UpgradeGemCost;
    public int StaminaRecover;
}


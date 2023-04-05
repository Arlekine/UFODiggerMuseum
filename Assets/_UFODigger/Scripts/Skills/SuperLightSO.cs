using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "SuperLight", menuName = "ScriptableObjects/Skills/SuperLight", order = 4)]
public class SuperLightSO : ScriptableObject
{
    public PLayerData PlayerData;
    public int SuperLightCount;
    public int LevelOfSkill;
    public int BuyGemCost;
    public Sprite SkillImage;
    public SuperLight[] SuperLightLevels;
    
    public string SaveKeyCount;
    public string SaveKeyLevel;
    public UnityEvent OnSkillChange;
    
    [TextArea] public string Info;
    
    public void RiseLevelByGems()
    {
        if (LevelOfSkill >= SuperLightLevels.Length -1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PlayerData.GemCount >= SuperLightLevels[LevelOfSkill + 1].UpgradeGemCost)
        {
            LevelOfSkill++;
            PlayerData.AddGems(-SuperLightLevels[LevelOfSkill].UpgradeGemCost);
            OnSkillChange.Invoke();
        }

        SaveSkillData();
    }
    public bool CheckIfMaxLevel()
    {
        return (LevelOfSkill >= SuperLightLevels.Length - 1);
    }
    
    public void SaveSkillData()
    {
        SaveLoadSystem.Save(SaveKeyCount,SuperLightCount);
        SaveLoadSystem.Save(SaveKeyLevel,LevelOfSkill);
    }

    public void LoadSkillData()
    {
        if (SaveLoadSystem.CheckKey(SaveKeyCount))
        {
            SuperLightCount =  SaveLoadSystem.LoadInt(SaveKeyCount);
        }
        else
        {
            SuperLightCount = 0;
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
            SuperLightCount++;
            SaveSkillData();
            OnSkillChange.Invoke();
        }
    }
    
}

[Serializable]
public class SuperLight
{
    public int UpgradeGemCost;
    public int IncomUp;
}
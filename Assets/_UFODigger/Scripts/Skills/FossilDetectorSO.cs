using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "Fossil", menuName = "ScriptableObjects/Skills/Fossil", order = 4)]
public class FossilDetectorSO : ScriptableObject,ISkill
{
    public PLayerData PlayerData;
    public int DetectorsCount;
    public int LevelOfSkill;
    public int BuyGemCost;
    public Sprite SkillImage;
    public FossilDetector[] DetectorLevels;
    public string DetectorDiscription;

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
        get { return DetectorsCount; }
        set { DetectorsCount = value; }
    }

    public string Description
    {
        get { return DetectorDiscription; }
        set { DetectorDiscription = value; }
    }

    public Sprite SkillSprite
    {
        get { return SkillImage; }
        set { SkillImage = value; }
    }

    public void Use()
    {
        if (DetectorsCount < 1)
            return;

        var partOnLayer = FindObjectOfType<PartOnLayer>();

        if (partOnLayer != null)
        {
            DetectorsCount--;
            partOnLayer.ShowDetectSota();
        }

        SaveSkillData();
    }

    public void RiseLevelByGems()
    {
        if (LevelOfSkill >= DetectorLevels.Length -1)
        {
            Debug.LogWarning("Try Upgrade for Max Level");
            return;
        }

        if (PlayerData.GemCount >= DetectorLevels[LevelOfSkill + 1].UpgradeGemCost)
        {
            LevelOfSkill++;
            PlayerData.AddGems(-DetectorLevels[LevelOfSkill].UpgradeGemCost);
            OnSkillChange.Invoke();
        }

        SaveSkillData();
    }
    public bool CheckIfMaxLevel()
    {
        return (LevelOfSkill >= DetectorLevels.Length - 1);
    }
    public void SaveSkillData()
    {
        SaveLoadSystem.Save(SaveKeyCount,DetectorsCount);
        SaveLoadSystem.Save(SaveKeyLevel,LevelOfSkill);
    }

    public void LoadSkillData()
    {
        if (SaveLoadSystem.CheckKey(SaveKeyCount))
        {
            DetectorsCount =  SaveLoadSystem.LoadInt(SaveKeyCount);
        }
        else
        {
            DetectorsCount = 0;
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
public class FossilDetector
{
    public int UpgradeGemCost;
    public int Accuracy;
}
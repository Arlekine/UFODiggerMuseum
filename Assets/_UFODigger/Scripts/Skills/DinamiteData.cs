using UnityEngine;
using UnityEngine.Events;
using Utils;

[CreateAssetMenu(fileName = "Dinamite", menuName = "ScriptableObjects/Skills/Dinamite", order = 4)]
public class DinamiteData : InstrumentData, ISkill
{
    
    public int DinamitCount;
    public int BuyGemCost;
    public Sprite SkillImage;
    public string DinamiteDescription;
    public string SkillSaveKey;
    public UnityEvent OnDinamiteBuy;
    public int Level
    {
        get { return LevelOfUpgrade; }
        set { LevelOfUpgrade = value; }
    }
    public int Count
    {
        get { return DinamitCount; }
        set { DinamitCount = value; }
    }

    public string Description
    {
        get { return DinamiteDescription; }
        set { DinamiteDescription = value; }
    }

    public Sprite SkillSprite
    {
        get { return SkillImage; }
        set { SkillImage = value; }
    }

    public void Use()
    {
        if (DinamitCount < 1)
            return;

        var playerInstrument = FindObjectOfType<PlayerInstrument>();

        if (playerInstrument != null)
        {
            DinamitCount--;
            playerInstrument.SetInstrumentOnOneTurn();
        }

        SaveSkillData();
    }

    public void SaveSkillData()
    {
        SaveLoadSystem.Save(SkillSaveKey,DinamitCount);
    }

    public void LoadSkillData()
    {
        if (SaveLoadSystem.CheckKey(SkillSaveKey))
        {
            DinamitCount =  SaveLoadSystem.LoadInt(SkillSaveKey);
        }
        else
        {
            DinamitCount = 0;
        }
    }

    public void BuySkill()
    {
        if (PLayerData.GemCount >= BuyGemCost)
        {
            PLayerData.AddGems(-BuyGemCost);
            Count++;
            SaveSkillData();
            OnDinamiteBuy.Invoke();
        }
    }
}
